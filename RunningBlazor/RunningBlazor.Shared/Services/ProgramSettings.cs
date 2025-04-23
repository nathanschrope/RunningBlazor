using RunningBlazor.Services;
using RunningBlazor.Shared.Running;

namespace RunningBlazor.Shared.Services;

public class ProgramSettings
{
    private IDataFactory<Setting> _settingFactory;
    private List<Setting> _settingList;
    public ProgramSettings(IDataFactory<Setting> dataFactory)
    {
        _settingList = dataFactory.GetAll().ToList();

        foreach (var prop in typeof(ProgramSettings).GetProperties())
        {
            var dbValue = _settingList.Where(x => string.Equals(prop.Name, x.Key)).FirstOrDefault();
            if (dbValue is not null)
            {
                if (prop.PropertyType == typeof(string))
                    prop.SetValue(this, dbValue.Value);
                else if (prop.PropertyType == typeof(bool))
                    prop.SetValue(this, bool.Parse(dbValue.Value));
                else if (prop.PropertyType == typeof(double))
                    prop.SetValue(this, double.Parse(dbValue.Value));
                else if (prop.PropertyType == typeof(int))
                    prop.SetValue(this, int.Parse(dbValue.Value));
                else
                    throw new NotImplementedException();
            }
        }

        _settingFactory = dataFactory;
    }

    public double StaticGoalMiles { get; private set; } = 6.25;

    public void UpdateStaticGoalMiles(double value)
    {
        string key = "StaticGoalMiles";
        StaticGoalMiles = value;

        UpdateProperty(key, value.ToString());
    }

    private void UpdateProperty(string key, string value)
    {
        var currentSetting = _settingList.Where(x => String.Equals(x.Key, key, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

        Setting setting;
        if (currentSetting is null)
        {
            setting = new Setting()
            {
                Key = key,
                Value = value.ToString()
            };
        }
        else
        {
            setting = new()
            {
                ID = currentSetting.ID,
                Key = key,
                Value = value.ToString()
            };
        }

        _settingFactory.Upsert(setting);

        _settingList = _settingFactory.GetAll().ToList();
    }
}
