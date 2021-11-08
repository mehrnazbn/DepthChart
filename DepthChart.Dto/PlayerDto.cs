using System;

namespace DepthChart.Dto
{
    public class PlayerDto
    {

        public Guid Id { get; set; }

     
        public TeamDto Team { get; set; }

        public string Name { get; set; }
    }
}

