﻿using Glasswall.Core.Engine.Common;
using Glasswall.Core.Engine.Common.GlasswallEngineLibrary;
using Glasswall.Core.Engine.Messaging;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Glasswall.Core.Engine
{
    [ExcludeFromCodeCoverage]
    public class WindowsEngineOperations : IGlasswallFileOperations, IDisposable
    {
        private const string Libc = @"lib\windows\glasswall.classic.dll";

        [DllImport(Libc, EntryPoint = "GWFileDone")]
        private static extern IntPtr GWFileDone();

        [DllImport(Libc, EntryPoint = "GWFileVersion")]
        private static extern IntPtr GWFileVersion();

        [DllImport(Libc, EntryPoint = "GWDetermineFileTypeFromFileInMem")]
        private static extern int GWDetermineFileTypeFromFileInMem(byte[] inputBuffer, int inputBufferSize);

        [DllImport(Libc, EntryPoint = "GWFileConfigGet")]
        private static extern int GWFileConfigGet(out IntPtr configurationBuffer, ref UIntPtr outputLength);

        [DllImport(Libc, EntryPoint = "GWFileConfigXML")]
        private static extern int GWFileConfigXML(IntPtr xmlConfig);

        [DllImport(Libc, EntryPoint = "GWMemoryToMemoryAnalysisAudit")]
        public static extern int GWMemoryToMemoryAnalysisAudit(
            byte[] inputBuffer,
            UIntPtr size,
            IntPtr type,
            out IntPtr outputBuffer,
            ref UIntPtr outputBufferSize);

        [DllImport(Libc, EntryPoint = "GWMemoryToMemoryProtect")]
        public static extern int GWMemoryToMemoryProtect(
            byte[] inputBuffer,
            UIntPtr size,
            IntPtr type,
            out IntPtr outputBuffer,
            ref UIntPtr outputBufferSize);

        [DllImport(Libc, EntryPoint = "GWFileErrorMsg")]
        public static extern IntPtr GWFileErrorMsg();

        [DllImport(Libc, EntryPoint = "GWGetAllIdInfo")]
        public static extern int GWGetAllIdInfo(
            out UIntPtr bufferLength,
            out IntPtr outPutBuffer);

        [DllImport(Libc, EntryPoint = "GWGetIdInfo")]
        public static extern IntPtr GWGetIdInfo(
            int issueID,
            out UIntPtr bufferLength,
            out IntPtr outPutBuffer);

        public EngineOutcome GetThreatCensorsAttributes(out string threats)
        {
            int result;
            threats = null;

            try
            {
                result = GWGetAllIdInfo(out UIntPtr outputBufferSizePtr, out IntPtr outputBuffer);

                if (outputBuffer != IntPtr.Zero && outputBufferSizePtr != UIntPtr.Zero)
                {
                    threats = Marshal.PtrToStringAnsi(outputBuffer, (int)outputBufferSizePtr);
                }
            }
            finally
            {
                GWFileDone();
            }
            return (EngineOutcome)result;
        }

        public string GetLibraryVersion()
        {
            IntPtr fileVersion = GWFileVersion();
            return fileVersion.MarshalNativeToManaged();
        }

        public string GetEngineError()
        {
            IntPtr response = GWFileErrorMsg();
            return response.MarshalNativeToManaged();
        }

        public FileType DetermineFileType(byte[] fileData)
        {
            int status = GWDetermineFileTypeFromFileInMem(fileData, fileData.Length);

            return (FileType)Enum.Parse(typeof(FileType), status.ToString(CultureInfo.InvariantCulture));
        }

        public EngineOutcome GetConfiguration(out string configuration)
        {
            UIntPtr configurationBufferLength = UIntPtr.Zero;

            int result = GWFileConfigGet(out IntPtr configurationBuffer, ref configurationBufferLength);

            configuration = configurationBuffer.MarshalNativeToManaged();

            return (EngineOutcome)result;
        }

        public EngineOutcome SetConfiguration(string configuration)
        {
            IntPtr nativeValue = configuration.MarshalManagedToNative();
            int result;

            try
            {
                result = GWFileConfigXML(nativeValue);
            }
            finally
            {
                nativeValue.CleanUpNativeData();
            }

            return (EngineOutcome)result;
        }

        public EngineOutcome AnalyseFile(byte[] fileContent, string fileType, out string analysisReport)
        {
            UIntPtr outputBufferSizePtr = UIntPtr.Zero;
            IntPtr nativeValue = fileType.MarshalManagedToNative();
            int result;

            try
            {
                result = GWMemoryToMemoryAnalysisAudit(fileContent, (UIntPtr)fileContent.Length, nativeValue, out IntPtr outputBufferPtr, ref outputBufferSizePtr);

                analysisReport = null;

                if (outputBufferPtr != IntPtr.Zero && outputBufferSizePtr != UIntPtr.Zero)
                {
                    analysisReport = Marshal.PtrToStringAnsi(outputBufferPtr, (int)outputBufferSizePtr);
                }
            }
            finally
            {
                nativeValue.CleanUpNativeData();
                GWFileDone();
            }

            return (EngineOutcome)result;
        }

        public EngineOutcome ProtectFile(byte[] fileContent, string fileType, out byte[] protectedFile)
        {
            UIntPtr outputBufferSizePtr = UIntPtr.Zero;
            IntPtr nativeValue = fileType.MarshalManagedToNative();
            int result;

            try
            {
                result = GWMemoryToMemoryProtect(fileContent, (UIntPtr)fileContent.Length, nativeValue, out IntPtr outputBufferPtr, ref outputBufferSizePtr);

                protectedFile = null;

                if (outputBufferPtr != IntPtr.Zero && outputBufferSizePtr != UIntPtr.Zero)
                {
                    protectedFile = new byte[(int)outputBufferSizePtr];
                    Marshal.Copy(outputBufferPtr, protectedFile, 0, protectedFile.Length);
                }
            }
            finally
            {
                nativeValue.CleanUpNativeData();
                GWFileDone();
            }

            return (EngineOutcome)result;
        }

        public string GetErrorMessage()
        {
            IntPtr errorMessage = GWFileErrorMsg();
            return errorMessage.MarshalNativeToManaged();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    GWFileDone();
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Since the class makes use of System.IntPtr the implememtation of a finalizer method is recommended
        /// </summary>
        ~WindowsEngineOperations()
        {
            Dispose(true);
        }
    }
}
