<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="AddComment.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Comment.AddComment" 
    meta:resourcekey="Page" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
    -
    <asp:Localize ID="Localize1" runat="server" meta:resourcekey="lclMenuExplanation" />
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <div id="form">
        <form id="AddCommentForm" method="post" runat="server">
            <br />
             <asp:TextBox ID="txtComment" runat="server" Width="300px" columns="35" meta:resourcekey="txtCommentResource1" ></asp:TextBox>
             <asp:RequiredFieldValidator ID="rfvComment" runat="server" ControlToValidate="txtComment"
                            Display="Dynamic"  meta:resourcekey="rfvCommentResource1"></asp:RequiredFieldValidator>
            <br/>
            <div class="button">
                <asp:Button ID="btnAddComment"  runat="server" OnClick="BtnAddCommentClick" meta:resourcekey="btnAddComment" />
            </div>
        </form>
    </div>
    <br/>
    <asp:Label ID="lblCommentOkey"  runat="server" style="color:red" meta:resourcekey="lblCommentOkey"></asp:Label>
    <br/>
</asp:Content>