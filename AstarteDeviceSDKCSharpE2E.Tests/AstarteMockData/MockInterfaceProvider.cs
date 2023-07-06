/*
 * This file is part of Astarte.
 *
 * Copyright 2023 SECO Mind Srl
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 * SPDX-License-Identifier: Apache-2.0
 */

using System.Collections.Generic;
using System.IO;
using System.Reflection;
using AstarteDeviceSDKCSharp;
using AstarteDeviceSDKCSharpE2E.Tests.Utilities;

namespace AstarteDeviceSDKCSharpE2E.Tests.AstarteMockData
{
    public class MockInterfaceProvider : IAstarteInterfaceProvider
    {
        private IAstarteDeviceMockData astarteMockData;
        private AstarteMockDevice astarteMockDevice;
        public MockInterfaceProvider()
        {
            astarteMockData = new AstarteDeviceMockData();
            astarteMockDevice = astarteMockData.GetAstarteMockData();
        }

        public List<string> LoadAllInterfaces()
        {
            List<string> interfaces = new List<string>();
            List<string> interfaceNames = new List<string>();

            if (astarteMockDevice is null)
            {
                return interfaces;
            }

            interfaceNames.Add(astarteMockDevice.InterfaceDeviceAggr);
            interfaceNames.Add(astarteMockDevice.InterfaceDeviceData);
            interfaceNames.Add(astarteMockDevice.InterfaceDeviceProp);
            interfaceNames.Add(astarteMockDevice.InterfaceServerAggr);
            interfaceNames.Add(astarteMockDevice.InterfaceServerData);
            interfaceNames.Add(astarteMockDevice.InterfaceServerProp);

            foreach (string interfaceName in interfaceNames)
            {
                interfaces.Add(LoadInterface(interfaceName));
            }

            return interfaces;
        }

        public string LoadInterface(string interfaceName)
        {
            string text =
                File.ReadAllText
                (Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
                , "Resources", "standard-interfaces", interfaceName + ".json"));

            return text;
        }
    }
}
