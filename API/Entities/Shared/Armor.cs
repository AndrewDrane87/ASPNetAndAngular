using System.ComponentModel.DataAnnotations.Schema;

namespace API;

public class Helmet
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ItemPhoto Photo { get; set; }
    public int ArmorValue { get; set; }
}

public class Armor
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ItemPhoto Photo { get; set; }
    public int ArmorValue { get; set; }
}

public class Boots
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ItemPhoto Photo { get; set; }
    public int ArmorValue { get; set; }
}
