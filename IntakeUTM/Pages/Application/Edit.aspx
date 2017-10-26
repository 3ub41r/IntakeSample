<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="IntakeUTM.Pages.Application.Edit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <h3>
        <asp:Literal ID="Name" runat="server"></asp:Literal>
    </h3>
    
    <asp:PlaceHolder ID="NotFound" runat="server" Visible="False">
        <div class="alert alert-danger">
            <p>Record not found.</p>
            <br>
            <asp:HyperLink ID="AddLink" NavigateUrl="~/Pages/Application/Add.aspx" CssClass="btn btn-default" runat="server">Add Application</asp:HyperLink>
        </div>
    </asp:PlaceHolder>

    <asp:PlaceHolder ID="Details" runat="server">
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
                <tr>
                    <td colspan="2">
                        <p><asp:Literal ID="OfferLetterText" runat="server"></asp:Literal></p>
                    </td>
                </tr>
            </tbody>
        </table>
    
        <div class="form-group">
            <asp:Button ID="GenerateLetterBtn" CssClass="btn btn-primary" runat="server" Text="Generate Offer Letter" />
            <asp:HyperLink ID="BottomAddLink" NavigateUrl="~/Pages/Application/Add.aspx" CssClass="btn btn-default" runat="server">Add Application</asp:HyperLink>
        </div>
    </asp:PlaceHolder>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterPlaceHolder" runat="server">
</asp:Content>
