namespace FindMyPG.Models
{
    
    
    public class StateModel:BaseModel
    {
        public string Name { get; set; }
        public List<CityModel> Cities { get; set; }
        
    }
    public class StateModelRequest
    {
        public StateModelRequest()
        {
            Cities = new List<CityModelRequest>();
        }
       public string StateName { get; set; }
        public bool IsActive { get; set; }
        public List<CityModelRequest> Cities { get; set; }
    }
    public class StateUpdateModelRequest
    {
        public string StateName { get; set; }
        public bool IsActive { set; get; }
    }
}
