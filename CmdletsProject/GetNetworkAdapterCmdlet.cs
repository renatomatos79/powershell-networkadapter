﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Management.Automation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CmdletsProject
{
    [Cmdlet(VerbsCommon.Get, nameof(NetworkAdapter))]
    [OutputType(typeof(NetworkAdapter))]
    public class GetNetworkAdapterCmdlet : Cmdlet
    {
        #region .: Parameters  :.

        [Parameter(Position = 0,
           ValueFromPipelineByPropertyName = true)]
        [Alias("nm")]
        public string Name { get; set; }

        [Parameter(Position = 1,
                   ValueFromPipelineByPropertyName = true,
                   ValueFromPipeline = true)]
        [ValidateCount(0, 3)]
        [Alias("mfg", "vendor")]
        public string[] Manufacturer { get; set; }

        [Parameter(Position = 2,
                   ValueFromPipelineByPropertyName = true)]
        [Alias("pa", "adapter")]
        public bool? PhysicalAdapter { get; set; }

        [Parameter(Position = 3)]
        [ValidateRange(0, 100)]
        [Alias("entries", "page", "mxe")]
        public int MaxEntries { get; set; } = 100;

        #endregion

        private IEnumerable<NetworkAdapter> _wmiResults;

        private static IEnumerable<NetworkAdapter> GetWmiResults()
        {
            const string wmiQuery = "Select * from Win32_NetworkAdapter";
            return new ManagementObjectSearcher(wmiQuery).Get()
                .Cast<ManagementObject>()
                .Select(BuildOutputObject);
        }

        protected override void BeginProcessing()
        {
            base.BeginProcessing();
            _wmiResults = GetWmiResults();
        }

        protected override void ProcessRecord()
        {
            var query = _wmiResults;
            if (Name != null)
            {
                query = query.Where(adapter =>
                    adapter.Name != null && adapter.Name.StartsWith(Name));
            }
            // Just like "Name" above, this checks for prefix matches
            // but on multiple values instead of just a single value.
            if (Manufacturer != null)
            {
                query = query.Where(
                    adapter =>
                        adapter.Manufacturer != null &&
                        Regex.IsMatch(adapter.Manufacturer,
                            string.Format("^(?:{0})", string.Join("|", Manufacturer))));
            }
            // Being a Boolean, an exact match is used here.
            if (PhysicalAdapter != null)
            {
                query = query.Where(adapter =>
                    adapter.PhysicalAdapter == PhysicalAdapter);
            }
            query.Take(MaxEntries).ToList().ForEach(WriteObject);
        }

        private static NetworkAdapter BuildOutputObject(ManagementBaseObject item)
        {
            return new NetworkAdapter
            {
                Name = (string)item[nameof(NetworkAdapter.Name)],
                Description = (string)item[nameof(NetworkAdapter.Description)],
                DeviceId = int.Parse((string)item[nameof(NetworkAdapter.DeviceId)]),
                Manufacturer = (string)item[nameof(NetworkAdapter.Manufacturer)],
                NetConnectionId = (string)item[nameof(NetworkAdapter.NetConnectionId)],
                PhysicalAdapter = (bool)item[nameof(NetworkAdapter.PhysicalAdapter)]
            };
        }
    }
}
