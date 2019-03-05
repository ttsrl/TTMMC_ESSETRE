using TTMMC_ESSETRE.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTMMC_ESSETRE.ConfigurationModels;

namespace TTMMC_ESSETRE.Models
{
    public interface IMachine 
    {
        int Id { get; }
        string Description { get; }
        string ReferenceName { get; }
        string Address { get; }
        string Port { get; }
        MachineType Type { get; }
        ConnectionProtocol ConnectionProtocol { get; }
        bool HaveImage { get; }
        bool Recording { get; set; }

        void Connect();
        MachineStatus GetStatus();
        string GetImageUrl();
        Task<T> ReadAsync<T>(string key);
        Task<string> ReadAsync(string key, Type type);
        T Read<T>(string key);
        string Read(string key, Type type);
        void Write<T>(string key, T item);
        Task WriteAsync<T>(string key, T item);
        Type GetDataItemType(DataItem data);
        Dictionary<string, List<DataItem>> GetParametersRead();
        Dictionary<string, List<DataItem>> GetParametersWrite();
        List<DataItem> GetParameterRead(string name);
        List<DataItem> GetParameterWrite(string name);
        KeyValuePair<string, List<DataItem>>? GetReferenceKeyWrite();
        KeyValuePair<string, List<DataItem>>? GetReferenceKeyRead();
    }

}
