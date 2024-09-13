namespace App.Services.Products;

public record ProductDto(int Id,string Name,decimal Price, int Stock,int CategoryId);





//product1==product2 bu şekilde bir karşılaştırma yapınca proplar aynı olsa dahi false döner. Çünkü referansları farklıdır.
//Bu yüzden de record kullanıyoruz ki karşılaştırma yapabilelim.
//Recordlar immutable'dır. Yani değerleri değiştirilemez. Değiştirmek istediğimizde yeni bir record oluşturmalıyız.
//Recordlar varsayılan olarak değer tipidir. Yani referansları farklı olsa dahi değerleri aynıysa true döner.
//Recordlar varsayılan olarak equatable'dır. Yani karşılaştırma yapabilirler.


//public record ProductDto
//{
//    public int Id { get; init; }
//    public string Name { get; init; }
//    public decimal Price { get; init; }
//    public int Stock { get; init; }
//}
