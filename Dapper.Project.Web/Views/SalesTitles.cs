namespace DapperProject.Web.Views;

<h2>Comments</h2>
    <a class="btn btn-success" href="@Url.Action("Create")">Create New</a>

    <table class="table">
    <thead>
    <tr>
    <th>Comment ID</th>
    <th>Product ID</th>
    <th>User ID</th>
    <th>Answer To</th>
    <th>Comment Text</th>
    <th>Comment Date</th>
    <th>Comment Type</th>
    <th>Comment Score</th>
    </tr>
    </thead>
    <tbody>
    @foreach (var comment in Model)
{
    <tr>
        <td>@comment.id</td>
        <td>@comment.product_id</td>
        <td>@comment.user_id</td>
        <td>@comment.answer_id</td>
        <td>@comment.comment_text</td>
        <td>@comment.comment_date?.ToString("yyyy-MM-dd")</td>
        <td>@comment.comment_type</td>
        <td>@comment.comment_score</td>
        <td>
        <a asp-action="Edit" asp-controller="Comments" asp-route-id="@comment.id" class="btn btn-primary">Edit</a>
        <a asp-action="Delete" asp-controller="Comments" asp-route-id="@comment.id" class="btn btn-danger">Delete</a>
        </td>
        </tr>
}
</tbody>
    </table>