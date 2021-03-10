# API Endpoints

## /api/rebuild
**Description:**
- This endpoint accepts requests to rebuild a file with Glasswall d-FIRST™ Engine. The request body contains the input file and output file URL and Glasswall Content Management Flags with a 'Content-Type' of 'application/json'. The Rebuilt file will be uploaded to output URL.

**Request:**

`POST /api/rebuild`

**Content type:** `application/json`

```
{
  "InputGetUrl": "string",
  "OutputPutUrl": "string",
  "ContentManagementFlags": {
    "PdfContentManagement": {
      "Metadata": 1,
      "InternalHyperlinks": 1,
      "ExternalHyperlinks": 1,
      "EmbeddedFiles": 1,
      "EmbeddedImages": 1,
      "Javascript": 1,
      "Acroform": 1,
      "ActionsAll": 1,
      "Watermark": "Glasswall Protected"
    },
    "ExcelContentManagement": {
      "Metadata": 1,
      "InternalHyperlinks": 1,
      "ExternalHyperlinks": 1,
      "EmbeddedFiles": 1,
      "EmbeddedImages": 1,
      "DynamicDataExchange": 1,
      "Macros": 1,
      "ReviewComments": 1
    },
    "PowerPointContentManagement": {
      "Metadata": 1,
      "InternalHyperlinks": 1,
      "ExternalHyperlinks": 1,
      "EmbeddedFiles": 1,
      "EmbeddedImages": 1,
      "Macros": 1,
      "ReviewComments": 1
    },
    "WordContentManagement": {
      "Metadata": 1,
      "InternalHyperlinks": 1,
      "ExternalHyperlinks": 1,
      "EmbeddedFiles": 1,
      "EmbeddedImages": 1,
      "DynamicDataExchange": 1,
      "Macros": 1,
      "ReviewComments": 1
    }
  }
}

```
## /api/rebuild/file

**Description:**
- This endpoint accepts requests to rebuild a file with Glasswall d-FIRST™ Engine. Both the file and the Content Management Policy are sent in the request body with a 'Content-Type' of 'multipart/form-data'. The Rebuilt file is then returned in the response body with a 'Content-Type' of 'application/octet-stream'.

**Request:**

`POST /api/rebuild/file`

**Content type:** `multipart/form-data`

**Parameters:**

- File
- contentManagementFlagJson ( Optional )
    - This field contains each of the Content Management Flags for the file types that the engine supports. The server treats this field as a JSON string. All the properties including the field itself are optional.
    
```
{
  "PdfContentManagement": {
    "Metadata": 1,
    "InternalHyperlinks": 1,
    "ExternalHyperlinks": 1,
    "EmbeddedFiles": 1,
    "EmbeddedImages": 1,
    "Javascript": 1,
    "Acroform": 1,
    "ActionsAll": 1,
    "Watermark": "Glasswall Protected"
  },
  "ExcelContentManagement": {
    "Metadata": 1,
    "InternalHyperlinks": 1,
    "ExternalHyperlinks": 1,
    "EmbeddedFiles": 1,
    "EmbeddedImages": 1,
    "DynamicDataExchange": 1,
    "Macros": 1,
    "ReviewComments": 1
  },
  "PowerPointContentManagement": {
    "Metadata": 1,
    "InternalHyperlinks": 1,
    "ExternalHyperlinks": 1,
    "EmbeddedFiles": 1,
    "EmbeddedImages": 1,
    "Macros": 1,
    "ReviewComments": 1
  },
  "WordContentManagement": {
    "Metadata": 1,
    "InternalHyperlinks": 1,
    "ExternalHyperlinks": 1,
    "EmbeddedFiles": 1,
    "EmbeddedImages": 1,
    "DynamicDataExchange": 1,
    "Macros": 1,
    "ReviewComments": 1
  }
}
```

## /api/rebuild/base64
- This endpoint accepts requests to rebuild a file with Glasswall d-FIRST™ Engine. The request body contains the Base64 representation of the file and Glasswall Content Management Flags with a 'Content-Type' of 'application/json'. A Base64 Representation of the rebuilt file is then returned in the response with a 'Content-Type' of 'text/plain'.

- Select a file below to copy its Base64 Encoded representation to clipboard and use it in request body. The Total supported request size of the API gateway is 6MB, therefore the base64 encoded string must also be less than 6MB.

**Request:**

`POST /api/rebuild/base64`

**Content type:** `application/json`

```
{
  "Base64": "string",
  "ContentManagementFlags": {
    "PdfContentManagement": {
      "Metadata": 1,
      "InternalHyperlinks": 1,
      "ExternalHyperlinks": 1,
      "EmbeddedFiles": 1,
      "EmbeddedImages": 1,
      "Javascript": 1,
      "Acroform": 1,
      "ActionsAll": 1,
      "Watermark": "Glasswall Protected"
    },
    "ExcelContentManagement": {
      "Metadata": 1,
      "InternalHyperlinks": 1,
      "ExternalHyperlinks": 1,
      "EmbeddedFiles": 1,
      "EmbeddedImages": 1,
      "DynamicDataExchange": 1,
      "Macros": 1,
      "ReviewComments": 1
    },
    "PowerPointContentManagement": {
      "Metadata": 1,
      "InternalHyperlinks": 1,
      "ExternalHyperlinks": 1,
      "EmbeddedFiles": 1,
      "EmbeddedImages": 1,
      "Macros": 1,
      "ReviewComments": 1
    },
    "WordContentManagement": {
      "Metadata": 1,
      "InternalHyperlinks": 1,
      "ExternalHyperlinks": 1,
      "EmbeddedFiles": 1,
      "EmbeddedImages": 1,
      "DynamicDataExchange": 1,
      "Macros": 1,
      "ReviewComments": 1
    }
  }
}
```

## /api/rebuild/zipfile

**Description:**
- This endpoint accepts requests to rebuild a zip file with Glasswall d-FIRST™ Engine. Both the file and the Content Management Policy are sent in the request body with a 'Content-Type' of 'multipart/form-data'. Rebuilt zip file is then returned in the response body with a 'Content-Type' of 'application/octet-stream'.

**Request:**

`POST /api/rebuild/zipfile`

**Content type:** `multipart/form-data`

**Parameters:**

- File 
    - The binary contents of the selected zip file will be uploaded to the server.
- contentManagementFlagJson ( Optional )
    - This field contains each of the Content Management Flags for the file types that the engine supports. The server treats this field as a JSON string. All the properties including the field itself are optional.
```
{
  "PdfContentManagement": {
    "Metadata": 1,
    "InternalHyperlinks": 1,
    "ExternalHyperlinks": 1,
    "EmbeddedFiles": 1,
    "EmbeddedImages": 1,
    "Javascript": 1,
    "Acroform": 1,
    "ActionsAll": 1,
    "Watermark": "Glasswall Protected"
  },
  "ExcelContentManagement": {
    "Metadata": 1,
    "InternalHyperlinks": 1,
    "ExternalHyperlinks": 1,
    "EmbeddedFiles": 1,
    "EmbeddedImages": 1,
    "DynamicDataExchange": 1,
    "Macros": 1,
    "ReviewComments": 1
  },
  "PowerPointContentManagement": {
    "Metadata": 1,
    "InternalHyperlinks": 1,
    "ExternalHyperlinks": 1,
    "EmbeddedFiles": 1,
    "EmbeddedImages": 1,
    "Macros": 1,
    "ReviewComments": 1
  },
  "WordContentManagement": {
    "Metadata": 1,
    "InternalHyperlinks": 1,
    "ExternalHyperlinks": 1,
    "EmbeddedFiles": 1,
    "EmbeddedImages": 1,
    "DynamicDataExchange": 1,
    "Macros": 1,
    "ReviewComments": 1
  }
}
```

## /api/rebuild/s3tozip

Rebuilds a zip file using S3 presignedURL/objectURL and This api requires s3 configuration(AWS key and secret) on the hosted server.

**Description:**
- This endpoint accepts requests to rebuild a zip file with Glasswall d-FIRST™ Engine. Both the presignedURL and the Content Management Policy are sent in the request body with a 'Content-Type' of 'multipart/form-data'. The Rebuilt file is then returned in the response body with a 'Content-Type' of 'application/octet-stream'.

**Request:**

`POST /api/rebuild/s3tozip`

**Content type:** `multipart/form-data`

**Parameters:**

- presignedURL 
    - The S3 presignedURL from where zip file will be downloaded.
- contentManagementFlagJson
    - This field contains each of the Content Management Flags for the file types that the engine supports. The server treats this field as a JSON string. All the properties including the field itself are optional.

```
{
  "PdfContentManagement": {
    "Metadata": 1,
    "InternalHyperlinks": 1,
    "ExternalHyperlinks": 1,
    "EmbeddedFiles": 1,
    "EmbeddedImages": 1,
    "Javascript": 1,
    "Acroform": 1,
    "ActionsAll": 1,
    "Watermark": "Glasswall Protected"
  },
  "ExcelContentManagement": {
    "Metadata": 1,
    "InternalHyperlinks": 1,
    "ExternalHyperlinks": 1,
    "EmbeddedFiles": 1,
    "EmbeddedImages": 1,
    "DynamicDataExchange": 1,
    "Macros": 1,
    "ReviewComments": 1
  },
  "PowerPointContentManagement": {
    "Metadata": 1,
    "InternalHyperlinks": 1,
    "ExternalHyperlinks": 1,
    "EmbeddedFiles": 1,
    "EmbeddedImages": 1,
    "Macros": 1,
    "ReviewComments": 1
  },
  "WordContentManagement": {
    "Metadata": 1,
    "InternalHyperlinks": 1,
    "ExternalHyperlinks": 1,
    "EmbeddedFiles": 1,
    "EmbeddedImages": 1,
    "DynamicDataExchange": 1,
    "Macros": 1,
    "ReviewComments": 1
  }
}
```

## /api/rebuild/s3tos3

**Description:**
This endpoint accepts requests to rebuild a zip file with Glasswall d-FIRST™ Engine. The presignedURL, targetPresignedURL and the Content Management Policy are sent in the request body with a 'Content-Type' of 'multipart/form-data'. The Rebuilt file is then uploaded to targetPresignedURL.

**Request:**

`POST /api/rebuild/s3tos3`

**Content type:** `multipart/form-data`

**Parameters:**

- sourcePresignedURL 
    - The S3 sourcePresignedURL from where zip file will be downloaded.
- targetPresignedURL
    - The S3 targetPresignedURL where rebuilt zip file will be uploaded.
- contentManagementFlagJson
    - This field contains each of the Content Management Flags for the file types that the engine supports. The server treats this field as a JSON string. All the properties including the field itself are optional.
    
```
  {
  "PdfContentManagement": {
    "Metadata": 1,
    "InternalHyperlinks": 1,
    "ExternalHyperlinks": 1,
    "EmbeddedFiles": 1,
    "EmbeddedImages": 1,
    "Javascript": 1,
    "Acroform": 1,
    "ActionsAll": 1,
    "Watermark": "Glasswall Protected"
  },
  "ExcelContentManagement": {
    "Metadata": 1,
    "InternalHyperlinks": 1,
    "ExternalHyperlinks": 1,
    "EmbeddedFiles": 1,
    "EmbeddedImages": 1,
    "DynamicDataExchange": 1,
    "Macros": 1,
    "ReviewComments": 1
  },
  "PowerPointContentManagement": {
    "Metadata": 1,
    "InternalHyperlinks": 1,
    "ExternalHyperlinks": 1,
    "EmbeddedFiles": 1,
    "EmbeddedImages": 1,
    "Macros": 1,
    "ReviewComments": 1
  },
  "WordContentManagement": {
    "Metadata": 1,
    "InternalHyperlinks": 1,
    "ExternalHyperlinks": 1,
    "EmbeddedFiles": 1,
    "EmbeddedImages": 1,
    "DynamicDataExchange": 1,
    "Macros": 1,
    "ReviewComments": 1
  }
}
```

## /api/rebuild/ziptos3

**Description:**
- This endpoint accepts requests to rebuild a zip file with Glasswall d-FIRST™ Engine. The file, targetPresignedURL/objectURL. and the Content Management Policy are sent in the request body with a 'Content-Type' of 'multipart/form-data'. The Rebuilt file is then uploaded to S3 targetPresignedURL

**Request:**

`POST /api/rebuild/ziptos3`

**Content type:** `multipart/form-data`

**Parameters:**
- file 
    - The binary contents of the selected zip file will be uploaded to the server.
- targetPresignedURL
    - The S3 targetPresignedURL where rebuilt zip file will be uploaded.
- contentManagementFlagJson
    - This field contains each of the Content Management Flags for the file types that the engine supports. The server treats this field as a JSON string. All the properties including the field itself are optional.
    
```
{
  "PdfContentManagement": {
    "Metadata": 1,
    "InternalHyperlinks": 1,
    "ExternalHyperlinks": 1,
    "EmbeddedFiles": 1,
    "EmbeddedImages": 1,
    "Javascript": 1,
    "Acroform": 1,
    "ActionsAll": 1,
    "Watermark": "Glasswall Protected"
  },
  "ExcelContentManagement": {
    "Metadata": 1,
    "InternalHyperlinks": 1,
    "ExternalHyperlinks": 1,
    "EmbeddedFiles": 1,
    "EmbeddedImages": 1,
    "DynamicDataExchange": 1,
    "Macros": 1,
    "ReviewComments": 1
  },
  "PowerPointContentManagement": {
    "Metadata": 1,
    "InternalHyperlinks": 1,
    "ExternalHyperlinks": 1,
    "EmbeddedFiles": 1,
    "EmbeddedImages": 1,
    "Macros": 1,
    "ReviewComments": 1
  },
  "WordContentManagement": {
    "Metadata": 1,
    "InternalHyperlinks": 1,
    "ExternalHyperlinks": 1,
    "EmbeddedFiles": 1,
    "EmbeddedImages": 1,
    "DynamicDataExchange": 1,
    "Macros": 1,
    "ReviewComments": 1
  }
}

```

# Response

| Code | Description |
|------|---------|
| 200 | OK - The requested file was processed by the Glasswall d-FIRST™ Engine |
| 400 | Bad Request - This is usually when the JSON input is malformed or missing parameters |
| 403 | Forbidden - This typically occurs when the JWT Token is not supplied, or it is incorrect. |
| 415 | Unsupported Media Type - This happens when the request was not sent in JSON. |
| 422 |	Unprocessable Entity - This occurs when the Glasswall Engine was unable to rebuild the file due to an unsupported file type or if it is non conforming. |

# Schemas

**ContentManagementPolicy**
- This field contains each of the Content Management Flags for the file types that the engine supports. The server treats this field as a JSON string. All the properties including the field itself are optional.

- Content Management Flag Key:


| flag | Description |
|------|---------    |
| 0    | Allow       |
| 1    | Sanitise (Default) |
| 2    | Disallow    |

**Forbidden**
	
- Forbidden - This typically occurs when the JWT Token is not supplied, or it is incorrect.

**UnsupportedMediaType**

- Unsupported Media Type - This happens when the request was not sent in JSON.

**UnprocessableEntity**
- Unsupported Media Type - This happens when the request was not sent in JSON.





