using Group3Project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Group3Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuiltPlantController : ControllerBase
    {
        continuousdbContext context = new continuousdbContext();

        //add to sandbox 
        [HttpPost("AddAPlant")]
        public Builtplant AddAPlant(int fuelId, int nameplateCapacity, string userId)
        {
            Builtplant newPlant = new Builtplant()
            {

                FuelId = fuelId,
                NameplateCapacity = nameplateCapacity,
                PowState = false

            };
            context.Builtplants.Add(newPlant);
            context.SaveChanges();

            Usertable table = new Usertable()
            {
                UserId = userId,
                BpId = context.Builtplants.OrderBy(b => b.Id).Last().Id
            };
            context.Usertables.Add(table);
            context.SaveChanges();
            return newPlant;
        }

        [HttpDelete("DestroyAPlant")]
        public Builtplant DestroyAPlant(int Id)
        {
            Builtplant removedPlant = context.Builtplants.FirstOrDefault(x => x.Id == Id);
            Usertable removedUT = context.Usertables.FirstOrDefault(x => x.BpId == Id);
            context.Builtplants.Remove(removedPlant);
            context.Usertables.Remove(removedUT);
            context.SaveChanges();
            return removedPlant;
        }

        //to speed this up, we should take in userId here, run GetAllPlants() once to return the user's plants. 
        [HttpGet("GetAllPlants")]
        public List<Builtplant> GetAllPlants()
        {
            return context.Builtplants.ToList();

        }
        // post - new object 
        // put - same obj, update fields 
        // patch - take an object, update props, submit them as a NEW entry 
        [HttpPut("ModifyCapacities/{Id}")]
        public Builtplant ModifyCapacities(int Id, int NPC, int AC)
        {
            Builtplant modifiedPlant = context.Builtplants.FirstOrDefault(x => x.Id == Id);
            modifiedPlant.Npc = NPC;
            modifiedPlant.Ac = AC;
            context.Builtplants.Update(modifiedPlant);
            context.SaveChanges();
            return modifiedPlant;

        }

        [HttpPut("FlipPowState")] 

        public Builtplant FlipPowState(int Id)
        {
            Builtplant turnOn = context.Builtplants.FirstOrDefault(x => x.Id == Id);
            turnOn.PowState = !turnOn.PowState;
            context.Builtplants.Update(turnOn);
            context.SaveChanges();
            return turnOn;
        }

        //or, we could take in user ID here. 
        [HttpGet("PlantAndMore")]
        public IEnumerable<Builtplant> PlantAndMore()
        {
            // pulling all plants from the database like normal, PlantProp property (fuel) is NULL for all. 
            List<Builtplant> plants = context.Builtplants.ToList();

            //dictionary : pages = fuel type, definition = PlantProp 
            Dictionary<int, Plantprop> plantsProp = context.Plantprops.ToDictionary(p => p.Id, p => p);

            //go through all plants from database 
            foreach (Builtplant iteratedPlant in plants)
            {
                // isolate the plant we care about (for each loop) 
                // set plant property 
                // find the corresponding plant property (Dictionary.Try(getValue) )
                // out - if value is found, where to put 


                if (iteratedPlant.FuelId != null)
                { //if we CAN get props from a fuel id (if it exists) (which it always will but just in 
                    Plantprop please = default; //get said fuel props 
                    bool didFindFuel = plantsProp.TryGetValue((int)iteratedPlant.FuelId, out please);
                    if (didFindFuel)
                    {
                        iteratedPlant.Fuel = please;
                    }
                }
            }
            return plants;
        }

        [HttpGet("PlantAndMoreByUserId/{userId}")]
        public IEnumerable<Builtplant> PlantAndMoreByUserId(string userId)
        {
            // pulling all plants from the database like normal, PlantProp property (fuel) is NULL for all. 
            

            List<Builtplant> results = new List<Builtplant>();

            List<Usertable> matches = context.Usertables.Where(u => u.UserId == userId).ToList();

            foreach(Usertable match in matches)
            {
                Builtplant builtPlant = context.Builtplants.FirstOrDefault(p => p.Id == match.BpId);
                results.Add(builtPlant);
                
            }
            
            //dictionary : pages = fuel type, definition = PlantProp 
            Dictionary<int, Plantprop> plantsProp = context.Plantprops.ToDictionary(p => p.Id, p => p);

            //go through all plants from database 
            foreach (Builtplant iteratedPlant in results)
            {
                // isolate the plant we care about (for each loop) 
                // set plant property 
                // find the corresponding plant property (Dictionary.Try(getValue) )
                // out - if value is found, where to put 


                if (iteratedPlant.FuelId != null)
                { //if we CAN get props from a fuel id (if it exists) (which it always will but just in 
                    Plantprop please = default; //get said fuel props 
                    bool didFindFuel = plantsProp.TryGetValue((int)iteratedPlant.FuelId, out please);
                    if (didFindFuel)
                    {
                        iteratedPlant.Fuel = please;
                    }
                }
            }
            return results;
        }
    }
}


