//-----------------------------------------------------------------------
// <copyright file="TRRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.IO;
using System.Xml.Serialization;
using TrafficRush.Model;

namespace TrafficRush.Repository
{
    /// <summary>
    /// Repository class.
    /// </summary>
    public class TRRepository : IRepository
    {
        private const string HIGH_SCORE_PATH = "score.txt";
        private const string STATE_PATH = "save.xml";
        private IModel model;
        private XmlSerializer serializer;

        /// <summary>
        /// Initializes a new instance of the <see cref="TRRepository"/> class.
        /// </summary>
        /// <param name="model">Model.</param>
        public TRRepository(IModel model)
        {
            this.model = model;
            serializer = new XmlSerializer(model.GetType());
        }

        /// <summary>
        /// Method for saving current state.
        /// </summary>
        public void SaveModelToXml()
        {
            using (StreamWriter writer = new StreamWriter(Path.GetFullPath(STATE_PATH)))
            {
                serializer.Serialize(writer, this.model);
            }
        }

        /// <summary>
        /// Method for loading saved state.
        /// </summary>
        public void LoadModelFromXml()
        {
            IModel newModel;
            using (StreamReader reader = new StreamReader(Path.GetFullPath(STATE_PATH)))
            {
                newModel = (IModel)serializer.Deserialize(reader);
            }

            this.model.Player = null;
            this.model.Traffic = null;
            this.model.Enemy = null;
            this.model.Score = 0;

            this.model.Player = newModel.Player;
            this.model.Enemy = newModel.Enemy;
            this.model.Traffic = newModel.Traffic;
            this.model.Score = newModel.Score;
        }

        /// <summary>
        /// Method for getting high score.
        /// </summary>
        /// <returns>High score.</returns>
        public double GetHighScore()
        {
            try
            {
                var data = File.ReadAllLines(HIGH_SCORE_PATH);
                return data.Length != 0 ? double.Parse(data[0]) : 0;
            }
            catch (FileNotFoundException)
            {
                return 0;
            }
        }

        /// <summary>
        /// Method for saving new high score.
        /// </summary>
        /// <param name="score">New high score.</param>
        public void SetHighScore(double score)
        {
            StreamWriter writer = new StreamWriter(HIGH_SCORE_PATH);
            writer.WriteLine(score);
            writer.Close();
        }
    }
}
