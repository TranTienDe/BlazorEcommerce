Link Source: Make an E-Commerce Website with Blazor WebAssembly in .NET 6
https://www.udemy.com/course/blazor-ecommerce/learn/lecture/29643746#overview

Section 01:
Section 02:
1. Trust the Dev Certificate
>dotnet dev-verts https --trust

2. Hot reload
>cd .\BlazorEcommerce\server
>dotnet wath run

10. Add the Product Model
	- Thêm vào file _import của client
		@using BlazorEcommerce.Shared;

	- Thêm vào file Program của Server
	    global using BlazorEcommerce.Shared;

11. First component
	- Add: ProductList.razor, ProductDetail.razor	

12. Css Isolation
	- Add css: ProductList.razor.css, ProductDetail.razor.css

15. Add Controller
	- Add: ProductController

16. Call the Web Api from client
17. Add  SwaggerUI
	- Server add packgage: Swashbuckle.AspNetCore
	- Config trong Program: 
	builder.Services.AddEndpointsApiExplorer();
	builder.Services.AddSwaggerGen();
	app.UseSwaggerUI();
	app.UseSwagger();

	- Link test: https://localhost:7124/swagger/index.html

19. Add the connectionstring
	- Add trong appsetting.json
	"ConnectionStrings": {
    "DefaultConnection" : "server=.;database=blazorecommerce;trusted_connection=true;user=sa;password=123456;"
    },

20. Add Entity Framework
	- Uninstall tool:
	>dotnet tool uninstall --global dotnet-ef

	- Install tool again:
	>dotnet tool install --global dotnet-ef

	- Check version dotnet tool
	>dotnet ef

	- Add nuget:
	Microsoft.EntityFrameworkCore
	Microsoft.EntityFrameworkCore.Design
	Microsoft.EntityFrameworkCore.SqlServer

22. Add DataContext
23. Register the DbContext and configure SqlServer
	builder.Services.AddDbContext<DataContext>(options =>
	{
		options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
	});

24. Your First Migration With Entity Framework
	>dotnet ef migrations add Initial
	>dotnet ef migrations remove
	>dotnet ef database update

25. Seed Data
	 protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Product>().HasData( new Product {} );
		}

26. GetProducts from the Database
	- Tạo suggest field
	Tool/Option/Text Editot/C#
	-> Add: _fieldName, _, camel case name
	-> Add: Suggest.

Section 3: Adding & Using Best Practices
30. Implement a Service Response with Generics
31. Using Services With Dependency Injection
32. add ProductService to the Client
47. Get Products by Category on the Client
	- Add param: GetProduct(string CategoryUrl);
	- Add event: event Action ProductsChanged;
	- Add param: @page "/{categoryurl}" tham số đầu vào trang
	- Khi load xong danh sách sản phẩm: ProductsChanged.Invoke()
	- Trong ProductList: hàm OnInitialized() đăng ký StateHasChanged;
	- @implements IDisposable: để hủy hàm StateHasChanged.

	protected override void OnInitialized()
    {
        ProductService.ProductsChanged += StateHasChanged;
    }

    public void Dispose()
    {
        ProductService.ProductsChanged -= StateHasChanged;
    }

70. Introducing the Cart with the Local Storage
Nội dung:
	- Install nuget for Client: Blazored.LocalStorage
	- Add in Program.cs: builder.Services.AddBlazoredLocalStorage();
	- Add in _imports.razor: @using Blazored.LocalStorage;

Section 5:
- Add model and razor: UserRegister.class, Register.razor 
- Add UserMenu: UserButton.razor
- Add Annotation to UserRegister.clas
- Add valid <DataAnnotationsValidator> to Register.razor
- Add <ValidattionSummary /> hiển thị lỗi.
- Hiển thị lỗi cho từng control: <ValidationMessage For="@(() => user.Email)" />

107. Implement a CustomAuthenticationStateProvider
- Add package to Client: Microsoft.AspNetCore.Components.Authorization
- Add class: CustomAuthStateProvider
	1. Lấy token từ LocalStorage.
	2. Tạo 1 ClaimsIdentity chứa thông user từ token.

- Add Program.cs:
	global using Microsoft.AspNetCore.Components.Authorization;

	builder.Services.AddOptions();
	builder.Services.AddAuthorizationCore();
	builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

110. Add a Return Url to the Login
	1. Add package to client: Microsoft.AspNetCore.WebUtilities
	2. Add tham số returnUrl trong nút chọn login
		<a href="login?returnUrl=@NavigationManager.ToBaseRelativePath(NavigationManager.Uri)" class="dropdown-item">Login</a>
	3. Lấy tham số returnUrl trong init login
	protected override void OnInitialized()
    {
        var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);
        if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("returnUrl", out var url))
        {
            returnUrl = url;
        }
    }
112. utilize the [Authorize] Attribute on the Client
Nội dung:
	1. Add to file _import.cs: @using Microsoft.AspNetCore.Authorization;
	2. Add Attribute: to razor file: @attribute [Authorize]
	3. Add text to App.razor: 
		<AuthorizeRouteView RouteData="@routeData" DefaultLayout="@typeof(ShopLayout)">
			<NotAuthorized>
				<h3>Whoops! You're not allowed to see this page.'</h3>
				<h5>Please <a href="login">login</a> or <a href="register">register</a> for a new account.</h5>
			</NotAuthorized>
		</AuthorizeRouteView>


