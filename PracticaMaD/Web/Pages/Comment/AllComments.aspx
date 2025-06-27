<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="AllComments.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Comment.AllComments" 
    meta:resourcekey="Page" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
    -
    <asp:Localize ID="Localize1" runat="server" meta:resourcekey="lclMenuExplanation" />
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <form id="formComments" method="POST" runat="server">
        <div class="field" > 
            <strong>
                <asp:Label ID="lblComments" meta:resourcekey="lblComments" runat="server" ></asp:Label><br/>
            </strong>
            <asp:Label ID="lblNoComments" meta:resourcekey="lblNoComments" runat="server" ></asp:Label><br/>

            <asp:GridView ID="gvComments" runat="server" CssClass="products"
                AutoGenerateColumns="False" align="center" OnRowDataBound="gvComments_RowDataBound" meta:resourcekey="gvCommentsResource1">
                <Columns>
                    <asp:BoundField DataField="commId" HeaderText="ID" Visible="False" />
                    <asp:BoundField DataField="body"  HeaderText="<%$ Resources: textComment %>" meta:resourcekey="BoundFieldResource2" />
                    <asp:BoundField DataField="commDate" HeaderText="<%$ Resources: date %>" meta:resourcekey="BoundFieldResource3" />
                    <asp:BoundField DataField="userName"  HeaderText="<%$ Resources: user %>" meta:resourcekey="BoundFieldResource4" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:HyperLink ID="lnkEdit" runat="server" 
                                NavigateUrl='<%# "~/Pages/Comment/EditComment.aspx?commId=" + Eval("commId") %>' 
                                Visible="true" HeaderText="<%$ Resources: editComment %>" meta:resourcekey="edit"></asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:HyperLink ID="lnkDelete" runat="server" 
                                NavigateUrl='<%# "~/Pages/Comment/DeleteComment.aspx?commId=" + Eval("commId") %>' 
                                Visible="true" HeaderText="<%$ Resources: deleteComment %>" meta:resourcekey="delete"></asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>  
        </div>
        <br />
        <!-- "Previous" and "Next" links. -->
        <div class="previousNextLinks">
            <span class="previousLink">
                <asp:HyperLink ID="lnkPrevious" Text="Previous >" runat="server"
                    Visible="False" meta:resourcekey="lnkPreviousResource1"></asp:HyperLink>
            </span><span class="nextLink">
                <asp:HyperLink ID="lnkNext" runat="server" Visible="False" meta:resourcekey="lnkNextResource1"></asp:HyperLink>
            </span>
        </div>
    </form>
    <br />
</asp:Content>