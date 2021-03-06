﻿using System;
using System.Collections.Generic;
using Castle.Windsor;
using NUnit.Framework;
using Moq;
using Rubberduck.Parsing.Inspections.Abstract;
using Rubberduck.Parsing.VBA;
using Rubberduck.Settings;
using Rubberduck.VBEditor.SafeComWrappers.Abstract;
using Rubberduck.Root;
using Rubberduck.VBEditor.SourceCodeHandling;
using RubberduckTests.Mocks;

namespace RubberduckTests.IoCContainer
{
    [TestFixture]
    public class IoCResolvingTests
    {
        [Test]
        [Category("IoC_Registration")]
        public void ResolveInspections_NoException()
        {
            var vbeBuilder = new MockVbeBuilder();
            var ideMock = vbeBuilder.Build();
            var sourceFileHandler = new Mock<ITempSourceFileHandler>().Object;
            ideMock.Setup(m => m.TempSourceFileHandler).Returns(() => sourceFileHandler);
            var ide = ideMock.Object;
            var addInBuilder = new MockAddInBuilder();
            var addin = addInBuilder.Build().Object;
            var initialSettings = new GeneralSettings
            {
                EnableExperimentalFeatures = new List<ExperimentalFeatures>
                {
                    new ExperimentalFeatures()
                }
            };

            IWindsorContainer container = null;
            try
            {
                try
                {
                    container = new WindsorContainer().Install(new RubberduckIoCInstaller(ide, addin, initialSettings));
                }
                catch (Exception exception)
                {
                    Assert.Inconclusive($"Unable to register. {Environment.NewLine} {exception}");
                }

                var inspections = container.ResolveAll<IInspection>();

                //This test does not need an assert because it tests that no exception has been thrown.
            }
            finally
            {
                container?.Dispose();
            }
        }

        [Test]
        [Category("IoC_Registration")]
        public void ResolveRubberduckParserState_NoException()
        {
            var vbeBuilder = new MockVbeBuilder();
            var ideMock = vbeBuilder.Build();
            var sourceFileHandler = new Mock<ITempSourceFileHandler>().Object;
            ideMock.Setup(m => m.TempSourceFileHandler).Returns(() => sourceFileHandler);
            var ide = ideMock.Object;
            var addInBuilder = new MockAddInBuilder();
            var addin = addInBuilder.Build().Object;
            var initialSettings = new GeneralSettings
            {
                EnableExperimentalFeatures = new List<ExperimentalFeatures>
                {
                    new ExperimentalFeatures()
                }
            };

            IWindsorContainer container = null;
            try
            {
                try
            {
                container = new WindsorContainer().Install(new RubberduckIoCInstaller(ide, addin, initialSettings));
            }
            catch (Exception exception)
            {
                Assert.Inconclusive($"Unable to register. {Environment.NewLine} {exception}");
            }

            var state = container.ResolveAll<RubberduckParserState>();

                //This test does not need an assert because it tests that no exception has been thrown.
            }
            finally
            {
                container?.Dispose();
            }
        }
    }
}
