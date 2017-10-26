<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Add.aspx.cs" Inherits="IntakeUTM.Pages.Application.Add" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <h3>Add Application</h3>
    
    <div class="form-group">
        <label>Name</label>
        <asp:TextBox ID="Name" CssClass="form-control" runat="server"></asp:TextBox>
    </div>
    
    <div class="form-group">
        <label>Email</label>
        <asp:TextBox ID="Email" CssClass="form-control" runat="server"></asp:TextBox>
    </div>
    
    <div class="form-group">
        <label>Applied Programme</label>
        <asp:DropDownList ID="ProgrammeId" CssClass="form-control" runat="server"></asp:DropDownList>
    </div>
    
    <div class="form-group">
        <label>Offered Programme</label>
        <asp:DropDownList ID="OfferedProgrammeId" CssClass="form-control" runat="server"></asp:DropDownList>
    </div>
    
    <asp:Button ID="SubmitBtn" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="SubmitBtn_Click" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterPlaceHolder" runat="server">
</asp:Content>
