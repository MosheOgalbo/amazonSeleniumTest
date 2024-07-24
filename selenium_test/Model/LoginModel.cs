
namespace DotnetSeleniumTest
{
  public class LoginModel{
    public string? Username { get; set; }
    public string? Password { get; set; }
    }
}


//     [Test]
 //  [Order(1)]
    // [Category("FirstTest")]
//     [TestCaseSource(nameof(Login))]
//     public void TestPOM(LoginModel  longModel) {
//         LoginPage loginPage = new LoginPage(_driver);
//         loginPage.Login(longModel.Username, longModel.Password);

//     }
// public static IEnumerable<LoginModel> Login() {
//     yield return new LoginModel(){
//         username = "admin",
//         password = "password"
//     };

// }
