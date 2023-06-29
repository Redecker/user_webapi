namespace backend_app.Models;

public class Result {
    public List<User>? results { get; set; }
}

public class User
{

    public class Name {
        public string? title { get; set; }
        public string? first { get; set; }
        public string? last { get; set; }
    }

    public class Location {
        public class Street {
            public int? number { get; set; }
            public string? name { get; set; }
        }
        public class Coordinates {
            public string? latitude { get; set; }
            public string? longitude { get; set; }
        }
        public class Timezone {
            public string? offset { get; set; }
            public string? description { get; set; }
        }
        public Street? street { get; set; }
        public string? city { get; set; }
        public string? state { get; set; }
        public string? country { get; set; }
        public string? postcode { get; set; }
        public Coordinates? coordinates { get; set; }
        public Timezone? timezone { get; set; }
    }

    public string? gender { get; set; }
    public Name? name { get; set; }
    public Location? location { get; set; }
    public string? email { get; set; }
}
