using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Price = table.Column<float>(type: "REAL", nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    category = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    username = table.Column<string>(type: "TEXT", nullable: false),
                    email = table.Column<string>(type: "TEXT", nullable: false),
                    role = table.Column<int>(type: "INTEGER", nullable: false),
                    password = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "InventoryProducts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    product_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    quantity = table.Column<uint>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryProducts", x => x.id);
                    table.ForeignKey(
                        name: "FK_InventoryProducts_Products_product_id",
                        column: x => x.product_id,
                        principalTable: "Products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HotDogStands",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    address = table.Column<string>(type: "TEXT", nullable: false),
                    operator_id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotDogStands", x => x.id);
                    table.ForeignKey(
                        name: "FK_HotDogStands_Users_operator_id",
                        column: x => x.operator_id,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    timesptamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    total = table.Column<double>(type: "REAL", nullable: false),
                    operator_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    user_id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.id);
                    table.ForeignKey(
                        name: "FK_Orders_Users_operator_id",
                        column: x => x.operator_id,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductsRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    OperatorId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductsRequests_Users_OperatorId",
                        column: x => x.OperatorId,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HotDogStandProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    StandId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProductId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotDogStandProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HotDogStandProducts_HotDogStands_StandId",
                        column: x => x.StandId,
                        principalTable: "HotDogStands",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HotDogStandProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrdersProducts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    order_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    product_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    quantity = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdersProducts", x => x.id);
                    table.ForeignKey(
                        name: "FK_OrdersProducts_Orders_order_id",
                        column: x => x.order_id,
                        principalTable: "Orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdersProducts_Products_product_id",
                        column: x => x.product_id,
                        principalTable: "Products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProductsRequestId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProductId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductRequests_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductRequests_ProductsRequests_ProductsRequestId",
                        column: x => x.ProductsRequestId,
                        principalTable: "ProductsRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "id", "category", "description", "name", "Price" },
                values: new object[] { new Guid("b3e1b27f-3de7-4dcc-883c-3d8cfb250004"), "HotDogs", "Basic hot dog with ketchup/mustard", "Hot Dog", 10f });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "id", "category", "description", "name", "Price" },
                values: new object[] { new Guid("bdb3791b-4552-4fce-a762-579018bbf4b9"), "HotDogs", "Hot dog with caramelized onions and ketchup", "Hot Onion Dog", 12.5f });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "id", "category", "description", "name", "Price" },
                values: new object[] { new Guid("ae1b6994-d923-4df5-a99f-8c53af5c2564"), "HotDogs", "Hot dog with melted gouda cheese and bacon", "Bacon Melt", 15f });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "id", "category", "description", "name", "Price" },
                values: new object[] { new Guid("00b00302-3a26-432d-9d5d-2d33eb52ee36"), "Extras", "Regular fries", "Fries", 7.5f });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "id", "category", "description", "name", "Price" },
                values: new object[] { new Guid("ad8ff4e2-0cb5-435a-bbc2-273ddf400f5a"), "Drinks", "Coke bottle", "Coke", 5f });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "email", "password", "role", "username" },
                values: new object[] { new Guid("8f41eea2-34f2-46ab-94b8-39197f82f4f4"), "customer@gmail.com", "B6C45863875E34487CA3C155ED145EFE12A74581E27BEFEC5AA661B8EE8CA6DD", 0, "customer" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "email", "password", "role", "username" },
                values: new object[] { new Guid("892c2469-e8b4-44d6-95ed-3414650ff8c7"), "admin@gmail.com", "8C6976E5B5410415BDE908BD4DEE15DFB167A9C873FC4BB8A81F6F2AB448A918", 3, "admin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "email", "password", "role", "username" },
                values: new object[] { new Guid("1cd11438-8cab-49c5-8b96-b8f2fe969aee"), "supplier@gmail.com", "955ED10B73D6265B1ADCF768B94F8DD5D91F33309DB94B6B3AF4EFA822F1D9AF", 2, "supplier" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "email", "password", "role", "username" },
                values: new object[] { new Guid("9abd2665-4a8c-45c0-bad0-f0039b98507d"), "operator1@gmail.com", "941E65AF88E0945C9F7DB5C306B0EF0FC5763DF6BFC9D339FF235195885083A2", 1, "operator1" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "email", "password", "role", "username" },
                values: new object[] { new Guid("5334bd44-635c-46b1-8c31-d4e89fa47ab1"), "operator2@gmail.com", "6EED3508EEE654F48CC4D57910EAD9310E4B2B386248D56BD40BBF16FCD9A77F", 1, "operator2" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "email", "password", "role", "username" },
                values: new object[] { new Guid("f1681e10-6fd8-46df-b3fc-66114afc355b"), "operator3@gmail.com", "0A722A639AB7D77124CDD29A0AD96FF421D50DC97A079705C4D5B2D97CF347B0", 1, "operator3" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "email", "password", "role", "username" },
                values: new object[] { new Guid("ab790609-5e1e-4a27-b49e-da6c0ef55ed9"), "operator4@gmail.com", "D71C5645CC3D232BCBD657888C4FF6AC0C6E33E2B89FB2162F8D96F276E8623A", 1, "operator4" });

            migrationBuilder.InsertData(
                table: "HotDogStands",
                columns: new[] { "id", "address", "operator_id" },
                values: new object[] { new Guid("db520e35-5e38-4409-a61f-3088ebb8d51e"), "Grimmer's Road", new Guid("9abd2665-4a8c-45c0-bad0-f0039b98507d") });

            migrationBuilder.InsertData(
                table: "HotDogStands",
                columns: new[] { "id", "address", "operator_id" },
                values: new object[] { new Guid("270be32e-bb1b-4a24-a416-e42233c3e122"), "Fieldfare Banks", new Guid("5334bd44-635c-46b1-8c31-d4e89fa47ab1") });

            migrationBuilder.InsertData(
                table: "HotDogStands",
                columns: new[] { "id", "address", "operator_id" },
                values: new object[] { new Guid("6fa4a929-bd97-424d-aacd-fd6dc95dc4e5"), "Imperial Passage", new Guid("f1681e10-6fd8-46df-b3fc-66114afc355b") });

            migrationBuilder.InsertData(
                table: "HotDogStands",
                columns: new[] { "id", "address", "operator_id" },
                values: new object[] { new Guid("b23a132c-7702-43a2-8824-47a7b811160b"), "Woodville Square", new Guid("ab790609-5e1e-4a27-b49e-da6c0ef55ed9") });

            migrationBuilder.InsertData(
                table: "HotDogStandProducts",
                columns: new[] { "Id", "ProductId", "Quantity", "StandId" },
                values: new object[] { new Guid("f1f62742-2282-482c-9853-fe8bd297a8dd"), new Guid("b3e1b27f-3de7-4dcc-883c-3d8cfb250004"), 7, new Guid("db520e35-5e38-4409-a61f-3088ebb8d51e") });

            migrationBuilder.InsertData(
                table: "HotDogStandProducts",
                columns: new[] { "Id", "ProductId", "Quantity", "StandId" },
                values: new object[] { new Guid("55a1173c-38ea-4fec-8e0a-ea770d2090c7"), new Guid("bdb3791b-4552-4fce-a762-579018bbf4b9"), 10, new Guid("db520e35-5e38-4409-a61f-3088ebb8d51e") });

            migrationBuilder.InsertData(
                table: "HotDogStandProducts",
                columns: new[] { "Id", "ProductId", "Quantity", "StandId" },
                values: new object[] { new Guid("ccdad75d-4c71-4cdb-860d-92f21fe20b9e"), new Guid("ae1b6994-d923-4df5-a99f-8c53af5c2564"), 13, new Guid("db520e35-5e38-4409-a61f-3088ebb8d51e") });

            migrationBuilder.InsertData(
                table: "HotDogStandProducts",
                columns: new[] { "Id", "ProductId", "Quantity", "StandId" },
                values: new object[] { new Guid("d1ee550e-e5e6-46f3-a19d-f289bc74e0e1"), new Guid("b3e1b27f-3de7-4dcc-883c-3d8cfb250004"), 20, new Guid("270be32e-bb1b-4a24-a416-e42233c3e122") });

            migrationBuilder.InsertData(
                table: "HotDogStandProducts",
                columns: new[] { "Id", "ProductId", "Quantity", "StandId" },
                values: new object[] { new Guid("bddb3746-9718-482e-8bc8-b8d03003e3de"), new Guid("bdb3791b-4552-4fce-a762-579018bbf4b9"), 6, new Guid("270be32e-bb1b-4a24-a416-e42233c3e122") });

            migrationBuilder.CreateIndex(
                name: "IX_HotDogStandProducts_ProductId",
                table: "HotDogStandProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_HotDogStandProducts_StandId",
                table: "HotDogStandProducts",
                column: "StandId");

            migrationBuilder.CreateIndex(
                name: "IX_HotDogStands_operator_id",
                table: "HotDogStands",
                column: "operator_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InventoryProducts_product_id",
                table: "InventoryProducts",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_operator_id",
                table: "Orders",
                column: "operator_id");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_user_id",
                table: "Orders",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersProducts_order_id",
                table: "OrdersProducts",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersProducts_product_id",
                table: "OrdersProducts",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRequests_ProductId",
                table: "ProductRequests",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRequests_ProductsRequestId",
                table: "ProductRequests",
                column: "ProductsRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsRequests_OperatorId",
                table: "ProductsRequests",
                column: "OperatorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HotDogStandProducts");

            migrationBuilder.DropTable(
                name: "InventoryProducts");

            migrationBuilder.DropTable(
                name: "OrdersProducts");

            migrationBuilder.DropTable(
                name: "ProductRequests");

            migrationBuilder.DropTable(
                name: "HotDogStands");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ProductsRequests");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
