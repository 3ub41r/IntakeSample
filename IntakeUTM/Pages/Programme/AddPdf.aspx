<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="AddPdf.aspx.cs" Inherits="IntakeUTM.Pages.Programme.AddPdf" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <h3>Add PDF Template</h3>
    
    <div class="form-group">
        <label>Name</label>
        <asp:TextBox ID="TemplateName" CssClass="form-control" runat="server"></asp:TextBox>
    </div>
    <div class="form-group">
        <label>Language</label>
        <asp:DropDownList ID="Language" CssClass="form-control" runat="server">
            <asp:ListItem>English</asp:ListItem>
            <asp:ListItem>Malay</asp:ListItem>
        </asp:DropDownList>
    </div>
    <div class="form-group">
        <label>Application Status</label>
        <asp:DropDownList ID="AppStatusListId" CssClass="form-control" runat="server">
        </asp:DropDownList>
    </div>
    <div class="form-group">
        <label>Sort Order</label>
        <asp:TextBox ID="SortOrder" CssClass="form-control" runat="server"></asp:TextBox>
    </div>
    
    <div class="form-group">
        <asp:FileUpload ID="PdfUpload" runat="server" />
    </div>

    <div class="form-group">
        <asp:Button ID="SubmitBtn" CssClass="btn btn-primary" runat="server" Text="Save" OnClick="SubmitBtn_Click" />
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterPlaceHolder" runat="server">
</asp:Content>
