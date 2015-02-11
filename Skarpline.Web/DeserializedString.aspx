<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DeserializedString.aspx.cs" Inherits="DeserializedString" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Skarpline Chat</title>

    <link href="styles/bootstrap.min.css" rel="stylesheet" />
    <link href="styles/font-awesome.css" rel="stylesheet" />
    <link href="styles/smartadmin-production.min.css" rel="stylesheet" />
    <style>
        .btn-md {
            padding: 5px 16px;
        }

        ul {
            padding: 0;
            margin: 0;
            width: 100%;
        }

            ul li {
                list-style: none;
                padding: 5px;
            }
    </style>
</head>
<body>
    <script src="scripts/jquery-1.10.2.min.js"></script>
    <div id="content">
        <div class="row">
            <div class="col-xs-12 col-sm-9 col-md-9 col-lg-9">
                <h1 class="page-title txt-color-blueDark">
                    <!-- PAGE HEADER -->
                    <i class="fa-fw fa fa-pencil-square-o"></i>
                    SKARPLINE
                    <span>

                        Chat app
                    </span>
                    <img id="loading" src="../Content/ajax-loader.gif" style="margin-left:40%; display:none;" />
                </h1>
            </div>
        </div>
        <!-- widget grid -->
        <section id="widget-grid" class="">
            <div class="row">
                <div class="col-sm-12 col-md-12 col-lg-12">
									<!-- well -->
									<div class="well">
										<h3 class="hidden-mobile">Object Deserialization</h3>
										
										<div class="table-responsive">
											
											<table class="table table-bordered table-striped hidden-mobile">
												<thead>
													<tr>
														<th>Type</th>
														<th>Result</th>
													</tr>
												</thead>
												<tbody>
													<tr>
														<td>Request string</td>
														<td><asp:Label ID="lblResponseString" runat="server"></asp:Label></td>
														
													</tr>
													<tr>
														<td>Object converstion</td>
														<td><asp:Label ID="lblConverstionResult" runat="server"></asp:Label></td>
														
													</tr>
												</tbody>
											</table>
											
										</div>
									</div>
									<!-- end well-->
				
								</div>
            </div>
        </section>
        <!-- end widget grid -->
    </div>
</body>
</html>
