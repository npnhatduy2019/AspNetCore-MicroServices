namespace Product.API.Extensions
{
    public static class ApplicationExtensions
    {
        public static void UseInfrastructure(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseRouting();

            //app.UseHttpsRedirection();

            app.UseAuthorization();

           app.UseEndpoints(endpoints =>
                    {
                        endpoints.MapControllers();
                        endpoints.MapControllerRoute(
                            name: "default",
                            pattern: "{controller=Home}/{action=Index}/{id?}"); // Thêm dòng này
                    });

        }

    }
}