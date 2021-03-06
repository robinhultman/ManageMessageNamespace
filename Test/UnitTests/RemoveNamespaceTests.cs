﻿using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.BizTalk.Component.Interop;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shared.PipelineComponents.ManageMessageNamespace.Tests.Constants;
using Winterdom.BizTalk.PipelineTesting;

namespace Shared.PipelineComponents.ManageMessageNamespace.Tests
{
    [TestClass]
    public class RemoveNamespaceTests
    {
        [TestMethod]
        public void RemoveQualifiedNamespace()
        {
            var removeNamespaceComponent = new RemoveNamespaceComponent();

            var components = new List<Tuple<IBaseComponent, PipelineStage>>
            {
                new Tuple<IBaseComponent, PipelineStage>(removeNamespaceComponent, PipelineStage.Validate)
            };

            var result = TestHelper.ExecuteReceivePipeline(TestFiles.QualifiedXmlFilePath, components);

            using (var reader = XmlReader.Create(result[0].BodyPart.Data))
            {
                reader.MoveToContent();
                Assert.IsTrue(reader.NamespaceURI == string.Empty, "Root node namespace is not removed");

                reader.MoveToElement();
                Assert.IsTrue(reader.NamespaceURI == string.Empty, "Child node namespace is not removed");
            }
        }

        public void RemoveUnqualifiedNamespace()
        {
            var removeNamespaceComponent = new RemoveNamespaceComponent();

            var components = new List<Tuple<IBaseComponent, PipelineStage>>
            {
                new Tuple<IBaseComponent, PipelineStage>(removeNamespaceComponent, PipelineStage.Validate)
            };

            var result = TestHelper.ExecuteReceivePipeline(TestFiles.UnqualifiedXmlFilePath, components);

            using (var reader = XmlReader.Create(result[0].BodyPart.Data))
            {
                reader.MoveToContent();
                Assert.IsTrue(reader.NamespaceURI == string.Empty, "Root node namespace is not removed");

                reader.MoveToElement();
                Assert.IsTrue(reader.NamespaceURI == string.Empty, "Child node namespace is not removed");
            }
        }

        public void RemoveQualifiedDefaultNamespace()
        {
            var removeNamespaceComponent = new RemoveNamespaceComponent();

            var components = new List<Tuple<IBaseComponent, PipelineStage>>
            {
                new Tuple<IBaseComponent, PipelineStage>(removeNamespaceComponent, PipelineStage.Validate)
            };

            var result = TestHelper.ExecuteReceivePipeline(TestFiles.QualifiedDefaultXmlFilePath, components);

            using (var reader = XmlReader.Create(result[0].BodyPart.Data))
            {
                reader.MoveToContent();
                Assert.IsTrue(reader.NamespaceURI == string.Empty, "Root node namespace is not removed");

                reader.MoveToElement();
                Assert.IsTrue(reader.NamespaceURI == string.Empty, "Child node namespace is not removed");
            }
        }

        [TestMethod]
        public void NonXmlContent()
        {
            var removeNamespaceComponent = new RemoveNamespaceComponent();

            TestHelper.TestFlatFile(removeNamespaceComponent);
        }
    }
}
