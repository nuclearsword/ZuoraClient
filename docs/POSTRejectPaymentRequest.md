# ZuoraClient.Model.POSTRejectPaymentRequest

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**GatewayResponse** | **string** | The transaction response returned by the gateway for this transaction. If the transaction was declined, this reason is provided in the message.  | [optional] 
**GatewayResponseCode** | **string** | Response message Code returned by the gateway about the transaction status.  | [optional] 
**ReferenceId** | **string** | Unique Id generated by the gateway for each transaction. Use this ID to find the respective Zuora Payment ID.   | [optional] 
**SecondReferenceId** | **string** | The second reference Id. Some gateway uses two unique transaction IDs.  | [optional] 
**SettledOn** | **DateTime** | The date and time of the transaction settlement. The format is &#x60;yyyy-mm-dd hh:mm:ss&#x60;.  | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)
