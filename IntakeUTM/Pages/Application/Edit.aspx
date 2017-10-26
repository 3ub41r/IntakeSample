<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="IntakeUTM.Pages.Application.Edit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <h3>
        <asp:Literal ID="Name" runat="server"></asp:Literal>
    </h3>
    
    <table class="table">
        <tbody>
        <tr>
            <th>Email</th>
            <td>
                <asp:Literal ID="Email" runat="server"></asp:Literal>
            </td>
        </tr>
            
        <tr>
            <th>Applied Programme</th>
            <td>
                <asp:Literal ID="AppliedProgramme" runat="server"></asp:Literal>
            </td>
        </tr>
            
        <tr>
            <th>Offered Programme</th>
            <td>
                <asp:Literal ID="OfferedProgramme" runat="server"></asp:Literal>
            </td>
        </tr>
        </tbody>
    </table>
    
    <div class="form-group">
        <asp:Button ID="GenerateLetterBtn" CssClass="btn btn-primary" runat="server" Text="Generate Offer Letter" />
        <a href="~/Pages/Application/Add.aspx" class="btn btn-default" title="Add New Application">Add Application</a>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterPlaceHolder" runat="server">
</asp:Content>
