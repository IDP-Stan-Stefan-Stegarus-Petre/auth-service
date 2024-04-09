using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MobyLabWebProgramming.Infrastructure.Migrations
{
    public partial class InitialDoi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Post_Id",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_User_Id",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_User_Id",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_FeedBack_User_Id",
                table: "FeedBack");

            migrationBuilder.DropForeignKey(
                name: "FK_Like_Post_Id",
                table: "Like");

            migrationBuilder.DropForeignKey(
                name: "FK_Like_User_Id",
                table: "Like");

            migrationBuilder.DropForeignKey(
                name: "FK_Post_User_Id",
                table: "Post");

            migrationBuilder.AddColumn<Guid>(
                name: "UserCreatorId",
                table: "Post",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PostId",
                table: "Like",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Like",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "FeedBack",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserCreatorId",
                table: "Event",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PostId",
                table: "Comment",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Comment",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Post_UserCreatorId",
                table: "Post",
                column: "UserCreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Like_PostId",
                table: "Like",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Like_UserId",
                table: "Like",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedBack_UserId",
                table: "FeedBack",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Event_UserCreatorId",
                table: "Event",
                column: "UserCreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_PostId",
                table: "Comment",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_UserId",
                table: "Comment",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Post_PostId",
                table: "Comment",
                column: "PostId",
                principalTable: "Post",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_User_UserId",
                table: "Comment",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Event_User_UserCreatorId",
                table: "Event",
                column: "UserCreatorId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FeedBack_User_UserId",
                table: "FeedBack",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Like_Post_PostId",
                table: "Like",
                column: "PostId",
                principalTable: "Post",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Like_User_UserId",
                table: "Like",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Post_User_UserCreatorId",
                table: "Post",
                column: "UserCreatorId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Post_PostId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_User_UserId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_User_UserCreatorId",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_FeedBack_User_UserId",
                table: "FeedBack");

            migrationBuilder.DropForeignKey(
                name: "FK_Like_Post_PostId",
                table: "Like");

            migrationBuilder.DropForeignKey(
                name: "FK_Like_User_UserId",
                table: "Like");

            migrationBuilder.DropForeignKey(
                name: "FK_Post_User_UserCreatorId",
                table: "Post");

            migrationBuilder.DropIndex(
                name: "IX_Post_UserCreatorId",
                table: "Post");

            migrationBuilder.DropIndex(
                name: "IX_Like_PostId",
                table: "Like");

            migrationBuilder.DropIndex(
                name: "IX_Like_UserId",
                table: "Like");

            migrationBuilder.DropIndex(
                name: "IX_FeedBack_UserId",
                table: "FeedBack");

            migrationBuilder.DropIndex(
                name: "IX_Event_UserCreatorId",
                table: "Event");

            migrationBuilder.DropIndex(
                name: "IX_Comment_PostId",
                table: "Comment");

            migrationBuilder.DropIndex(
                name: "IX_Comment_UserId",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "UserCreatorId",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "PostId",
                table: "Like");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Like");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "FeedBack");

            migrationBuilder.DropColumn(
                name: "UserCreatorId",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "PostId",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Comment");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Post_Id",
                table: "Comment",
                column: "Id",
                principalTable: "Post",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_User_Id",
                table: "Comment",
                column: "Id",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Event_User_Id",
                table: "Event",
                column: "Id",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FeedBack_User_Id",
                table: "FeedBack",
                column: "Id",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Like_Post_Id",
                table: "Like",
                column: "Id",
                principalTable: "Post",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Like_User_Id",
                table: "Like",
                column: "Id",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Post_User_Id",
                table: "Post",
                column: "Id",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
