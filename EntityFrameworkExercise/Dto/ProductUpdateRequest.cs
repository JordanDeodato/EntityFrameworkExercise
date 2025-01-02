﻿namespace EntityFrameworkExercise.Dto
{
    public class ProductUpdateRequest
    {
        public long Id { get; set; }
        public required string Name { get; set; }
        public required decimal Price { get; set; }
    }
}
