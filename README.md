# **RICH CONNECTOR**

#

[1. Setup Update Version 2](#_Toc142345679)

[a) Cài đặt thông tin Project RICH Connector 2](#_Toc142345680)

[b) Cài đặt thông tin Project Setup 2](#_Toc142345681)

[c) Cài đặt thông tin cho phần mềm tự động cập nhật 3](#_Toc142345682)

[2. API Print HTML 4](#_Toc142345683)

[3. API Show Display Order 4](#_Toc142345684)


# Setup Update Version


## Cài đặt thông tin Project RICH Connector

Các thông số trước khi tạo file setup sẽ là version cao hơn so với version của khách hàng đang dung.

Ví dụ khách hàng đang dùng version 1.1.1.0 thì lúc này phần mềm trước khi tạo file set-up phải điều chỉnh lại các thông số version **cao hơn** ví dụ ở đây là version 1.1.2.0

Cập nhật thông tin **Package version**

![image](https://github.com/VinNgoDu/RICHConnector/assets/89438689/7e643a67-7f3d-40d4-827f-dc217203ef0c)

Cập nhật **Assembly version & file version**

![image](https://github.com/VinNgoDu/RICHConnector/assets/89438689/844c135c-958e-4071-9c88-6990ba1d61ad)


## Cài đặt thông tin Project Setup

Cập nhật thông tin **Version & Product Code**

![image](https://github.com/VinNgoDu/RICHConnector/assets/89438689/52190131-1ab1-41c1-9eb9-245db93f4b5d)




1. Chọn project RICH Setup
2. Thay đổi Version
3. Chọn “Yes” để thay đổi Product Code

Cài đặt các thông số tương thích rồi bấm build file setup.


## Cài đặt thông tin cho phần mềm tự động cập nhật

Phần mềm sẽ đọc 1 link web chứa các thông tin cập nhật bao gồm

````xml
<?xml version="1.0" encoding="UTF-8"?>
<item>
  <version></version>
  <url></url>
  <changelog></changelog>
  <mandatory></mandatory>
</item>
````

| **Property** | **Type** | **Required** | **Note** |
| --- | --- | --- | --- |
| Version | string | Y | Bạn cần cung cấp phiên bản mới nhất của ứng dụng giữa các thẻ phiên bản. Phiên bản phải ở định dạng X.X.X.X. |
| url | string | Y | Bạn cần cung cấp URL của tệp cài đặt phiên bản mới nhất hoặc tệp zip giữa các thẻ url. AutoUpdater.NET tải xuống tệp được cung cấp tại đây và cài đặt nó khi người dùng nhấn nút Cập nhật. |
| changelog | string | N | Bạn cần cung cấp URL của nhật ký thay đổi của ứng dụng giữa các thẻ nhật ký thay đổi. Nếu bạn không cung cấp URL của nhật ký thay đổi thì hộp thoại cập nhật sẽ không hiển thị nhật ký thay đổi. |
| mandatory | bool | N | Bạn có thể đặt giá trị này thành **true** nếu không muốn người dùng bỏ qua phiên bản này. Thao tác này sẽ bỏ qua các tùy chọn Nhắc sau và Bỏ qua, đồng thời ẩn cả nút Bỏ qua và Nhắc sau trên hộp thoại cập nhật. |

# API Print HTML

![](RackMultipart20230812-1-vhnuvy_html_a91b06c48b082e7c.png)

Endpoint: POST 'http://localhost:5000/print/v2/print-html'
```json
  {
    "htmlContent": "<!DOCTYPE html><html>  <head>    <style>      body {        padding: 0px 0px;        margin: 0;        width: 270px;        font-family: Arial;      }      h5 {        margin: 4px 0;        font-size: 14px      }      h6 {        font-size: 13px;        margin: 4px 0      }      table {        width: 80%;        margin: 0 auto;        border-collapse: collapse;      }      div,      span,      strong {        font-size: 13px;        font-weight: 400      }      .text-center {        text-align: center      }      .row {}      .width-400 {        width: 270px;      }      .hr {        margin: 0 auto;        overflow: hidden;        width: 100%;        border: 1px dashed #000000;      }      .no-wrap {        white-space: nowrap;      }      .content {        width: 100%;      }      .line {        border-top: 1px solid;        padding-top: 10px      }      .subcontent {        width: 100%;        margin: 0 auto;      }      .text-right {        float: right;        text-align: right      }      .leftBit {        margin-left: -4px      }      .tdTicketItem {        margin-left: 10px      }      .black-box {        background: black;        color: white      }      @media print {        body {            padding: 0 10px;        }    }    @page {        margin: 0;    }    </style>  </head>  <body>    <div class='subcontent'>      <h5 class='text-center'>Technicans Report</h5>      <h6>John Doe</h6>      <div>Contracted: Yes% </div>      <div>2023-07-01 - 2023-07-31</div>      <br />      <div>Ticket: Ticket_001</div>      <div>Service: $100,00</div>      <div>Supply Fee: $50,00</div>      <div>Gratuity: $15,00 - 10%% = $20,00</div>      <br />    </div>    <div class=''>      <table>        <tr>          <td>            <h6>Day</h6>          </td>          <td>            <h6>Service</h6>          </td>          <td>            <h6>Tip</h6>          </td>        </tr>        <tr>          <td>            <span>2023-07-01</span>          </td>          <td>            <span>$100,00</span>          </td>          <td>            <span>$5,00</span>          </td>        </tr>        <tr>          <td>            <span>2023-07-15</span>          </td>          <td>            <span>$100,00</span>          </td>          <td>            <span>$10,00</span>          </td>        </tr>      </table>    </div>    <div class='subcontent'>      <hr />      <h6>Discounts</h6>      <div>Owner & Technician (OE) = $200,00</div>      <div>Owner = $50,00</div>      <div>Technician = $100,00</div>      <div>Owner Only (O) = $100,00</div>      <div>Technician Only (E) = $100,00</div>      <br />      <h6>First $1200 or Yes% = $300</h6>      <div>#Tips: 25</div>      <div>Total Tip: $55,00</div>      <br />      <h6>Payroll:</h6>      <div>Check: $1.500,00</div>      <div> Cash: $500,00</div>    </div>  </body></html>"
  }
```

| **Property** | **Type** | **Required** | **Note** |
| --- | --- | --- | --- |
| htmlContent | string | Y | Bao gồm html, css cho mẫu in bạn muốn in |


# API Show Display Order

Endpoint: POST 'http://localhost:5000/show-display-order'

Body order:
```json
 {

"id": "d17acdf2-515d-4b1e-aad8-d834e00eab83",

"currency": null,

"employee": null,

"subtotal": "$5.29",

"tax": "$0.37",

"total": "$5.66",

"title": "601e0e05-f3e9-443f-a6e2-7474d38d8caf",

"note": null,

"serviceChargeName": null,

"serviceChargeAmount": null,

"discounts": {

"elements": [

{

"id": null,

"lineItemId": null,

"name": "10% Off",

"amount": "$0.59",

"percentage": "10"

}

]

},

"lineItems": {

"elements": [

{

"id": "4d05e7b4-1379-4399-9065-ee8e3bb60b5e",

"orderId": null,

"name": "French Fries - Small ",

"alternateName": null,

"price": "$1.89",

"unitPrice": null,

"quantity": "1",

"unitQuantity": null,

"note": null,

"printed": false,

"binName": null,

"userData": null,

"discounts": {

"elements": []

},

"discountAmount": null,

"exchanged": false,

"exchangedAmount": null,

"modifications": {

"elements": []

},

"refunded": false,

"refundedAmount": null,

"percent": null

},

{

"id": "85dbcb80-445a-49ca-885d-2e91a506e58c",

"orderId": null,

"name": "Milk Shake - Strawberry ",

"alternateName": null,

"price": "$3.99",

"unitPrice": null,

"quantity": "1",

"unitQuantity": null,

"note": null,

"printed": false,

"binName": null,

"userData": null,

"discounts": {

"elements": []

},

"discountAmount": null,

"exchanged": false,

"exchangedAmount": null,

"modifications": {

"elements": []

},

"refunded": false,

"refundedAmount": null,

"percent": null

}

]

},

"amountRemaining": null,

"payments": {

"elements": []

}

}
```

Explain about field:

**Order**

| Field | Type | Description | Example |
| --- | --- | --- | --- |
| id | string | Id of this Order | d17acdf2-515d-4b1e-aad8-d834e00eab83 |
| currency | string | Currency this Order | null |
| employee | string | Staff name calculate this Order | Mon / Calvin / Tom, … (null) |
| subtotal | string | Summaries total all item non tax and discount. | "$5.29" |
| tax | string |
 | "$0.37" |
| total | string | Summaries total all item both tax and discount. | "$5.66" |
| title | string | Title of this Order | "601e0e05-f3e9-443f-a6e2-7474d38d8caf" |
| note | string | Note for this Order | null |
| discounts | Discounts | List elements Discount | null |
| lineItems | LineItems | List elements Items | null |
| amountRemaining | string |
 | null |
| payments | Payments | List elements payments | null |
| serviceChargeName | string | Service charge name | null |
| serviceChargeAmount | string | Service charge amount | null |

**Discount**

| Field | Type | Description | Example |
| --- | --- | --- | --- |
| id | string | Id of that discount | null |
| lineItemId | string | Use just separate lineItemId | null |
| name | string | Name of that discount | "10% Off" |
| amount | string | Amount display discount | "$0.59" |
| percentage | string | Percentage discount | "10" |

**LineItems**

| Field | Type | Description | Example |
| --- | --- | --- | --- |
| id | string | Id of that Order Item | "4d05e7b4-1379-4399-9065-ee8e3bb60b5e" |
| orderId | string | That orderId | null |
| name | string | Name of that Item | "French Fries - Small " |
| alternateName | string | Alternate name that Item | null |
| price | string | That price Item | "$1.89" |
| unitPrice | string |
 | null |
| quantity | string |
 | "1" |
| unitQuantity | string |
 | null |
| note | string | Note of that item | null |
| printed | bool |
 | false/true/null |
| binName | string |
 | null |
| userData | string |
 | null |
| discounts | Discounts | List elements Discount | null |
| discountAmount | string |
 | null |
| exchanged | bool |
 | false/true/null |
| exchangedAmount | string |
 | null |
| modifications | DisplayModification | List Display Modification | null |
| refunded | bool |
 | false/true/null |
| refundedAmount | string |
 | null |
| percent | string | Percentage that item | null |

**Payments**

| Field | Type | Description | Example |
| --- | --- | --- | --- |
| id | string | Id of this payment | |
| label | string | Label of this payment | "Payment 1" | |
| amount | string | Amount of this payment | "$1.23"||
| tipAmount | string | | ||
| taxAmount | string |  | ||

**DisplayModification**

| Field | Type | Description | Example |
| --- | --- | --- | --- |
| id | string | id of that modification | |
| name | string | Name of item modifier | "Item Modifier 1"/ null | |
| amount | string | Amount change | "+$3" ||
