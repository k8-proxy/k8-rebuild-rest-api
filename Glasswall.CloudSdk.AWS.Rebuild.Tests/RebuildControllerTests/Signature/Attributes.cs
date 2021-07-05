using Glasswall.CloudSdk.AWS.Rebuild.Controllers;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Linq;
using System.Reflection;

namespace Glasswall.CloudSdk.AWS.Rebuild.Tests.RebuildControllerTests.Signature
{
    [TestFixture]
    public class Attributes
    {
        [Test]
        public void Valid_Arguments_Should_Construct()
        {
            Attribute[] attributes = typeof(RebuildController).GetCustomAttributes().ToArray();

            Assert.That(attributes, Has.Exactly(2).Items);

            Assert.That(attributes,
                Has.Exactly(1)
                    .InstanceOf<RouteAttribute>()
                    .With
                    .Property(nameof(RouteAttribute.Template))
                    .EqualTo("api/[controller]"));

            Assert.That(attributes,
                Has.Exactly(1)
                    .InstanceOf<ControllerAttribute>());
        }
    }
}