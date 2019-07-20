# yungching_Interview_test

一個被呼叫後可以執行 SQL 的 API，

API 的入口分為兩個，

一個為需要回傳 Datatable 的入口(R用)，

一個為只需要執行只回傳執行後第一行的結果(CUD用)，


使用方式如下，

將 Web_API 加入 Web 參考，

將所附的 Common 元件加入參考，

將 API 原件 New 起，

將需要傳入的參數使用 Common.SQLParameter.ListSQLParameter New 起，

並指定適當的屬性，

並加入 List<Common.SQLParameter.ListSQLParameter> 中，

最後將 SQL 語法與 List<Common.SQLParameter.ListSQLParameter> 物件分別轉為 JSON 字串，

再將 JSON 字串使用 Base64 加密，

傳入 API 執行。


若為 SELECT 時，

則將 API 執行結果進行 Base64 解密，

並將得到的字串使用 (DataTable)JsonConvert.DeserializeObject 轉換為 Datatable，


詳細請參考 CRUD_Test.cs。