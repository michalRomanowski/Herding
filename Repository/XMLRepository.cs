using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using System.IO;
using Agent;
using Simulations;
using Teams;
using Auxiliary;
using Simulations.Parameters;

namespace Repository
{
    public class XMLRepository : ISimulationRepository
    {
        private readonly string directory;

        public XMLRepository() : this("") { }

        public XMLRepository(string directory)
        {
            this.directory = Path.Combine(directory, "Data");
            Directory.CreateDirectory(this.directory);
        }
        
        public void Save(string name, OptimizationParameters parameters, Population population)
        {
            Save(name, parameters);
            Save(name, population);
        }

        public void Save(string name, OptimizationParameters parameters)
        {
            Logger.Instance.AddLine($"Saving {name} optimization parameters");

            Directory.CreateDirectory(Path.Combine(directory, name));

            Save(parameters, Path.Combine(directory, name, typeof(OptimizationParameters).Name));
        }
        
        public void Save(string name, Population population)
        {
            Directory.CreateDirectory(Path.Combine(directory, name));

            for (int i = 0; i < population.Units.Count; i++)
            {
                Save(population.Units[i].Members, Path.Combine(directory, name, i.ToString()));
            }

            Save(population.Best.Members, Path.Combine(directory, name, "Best"));
        }

        public OptimizationParameters LoadOptimizationParameters(string name)
        {
            Logger.Instance.AddLine($"Loading {name} optimization parameters");

            return Load<OptimizationParameters>(Path.Combine(directory, name, $"{typeof(OptimizationParameters).Name}.xml"));
        }

        public OptimizationParameters LoadOptimizationParametersByPath(string path)
        {
            return Load<OptimizationParameters>(path);
        }

        public Population LoadPopulation(string name, IPopulationParameters parameters)
        {
            return LoadPopulationByPath(Path.Combine(directory, name), parameters);
        }

        public Population LoadPopulationByPath(string path, IPopulationParameters parameters)
        {
            Logger.Instance.AddLine($"Loading {path} population");

            var population = new Population();

            for (int i = 0; i < parameters.PopulationSize; i++)
            {
                population.Units.Add(LoadTeam(Path.Combine(path, i.ToString()), parameters));
            }

            population.Best = LoadTeam(Path.Combine(path, "Best"), parameters);

            return population;
        }

        public IEnumerable<string> GetSimulations()
        {
            return Directory.EnumerateDirectories(directory);
        }

        public string GetNewestSimulationName()
        {
            return GetSimulations()
                .Select(x => x.Split('\\').Last())
                .Where(x => long.TryParse(x, out _))
                .Max();
        }

        public Team LoadTeam(string path, ITeamParameters parameters)
        {
            var team = TeamFactory.GetTeam(parameters);

            for (int i = 0; i < parameters.NumberOfShepherds; i++)
            {
                team.Members[i] = Load<Shepherd>(Path.Combine(path, i.ToString()) + ".xml");
            }

            return team;
        }

        public Shepherd LoadShepherd(string path)
        {
            return Load<Shepherd>(path);
        }

        private void Save<T>(List<T> collection, string collectionDirectory)
        {
            Directory.CreateDirectory(collectionDirectory);

            for(int i = 0; i < collection.Count; i++)
            {
                Save(collection[i], Path.Combine(collectionDirectory, i.ToString()));
            }
        }

        private void Save(object data, string fullPath)
        {
            using (var s = new FileStream($"{fullPath}.xml", FileMode.Create))
            {
                new XmlSerializer(data.GetType()).Serialize(s, data);
            }
        }

        private T Load<T>(string fullPath)
        {
            using (var s = new FileStream(fullPath, FileMode.Open))
            {
                return (T)new XmlSerializer(typeof(T)).Deserialize(s);
            }
        }
    }
}
