# ZuoraClient.Model.POSTPaymentMethodDecryption

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**AccountID** | **string** | The ID of the customer account associated with this payment method. To create an orphan payment method that is not associated with any customer account, you do not need to specify this field during creation. However, you must associate the orphan payment method with a customer account within 10 days. Otherwise, this orphan payment method will be deleted. | [optional] 
**CardHolderInfo** | [**CreatePaymentMethodCardholderInfo**](CreatePaymentMethodCardholderInfo.md) |  | [optional] 
**IntegrationType** | **string** | Field to identify the token decryption type.  **Note:** The only value at this time is &#x60;ApplePay&#x60;.   | 
**InvoiceId** | **string** | The id of invoice this payment will apply to.  **Note:** When &#x60;processPayment&#x60; is &#x60;true&#x60;, this field is required. Only one invoice can be paid; for scenarios where you want to pay for multiple invoices, set &#x60;processPayment&#x60; to &#x60;false&#x60; and call payment API separately.  | [optional] 
**MerchantID** | **string** | The Merchant ID that was configured for use with Apple Pay in the Apple iOS Developer Center.  | 
**MitConsentAgreementSrc** | **string** | This field is only available for CyberSource v2.0 integration to create a stored credential profile within the payment method. It is required if the &#x60;mitProfileAction&#x60; field is specified.  When creating an Apple Pay payment method on CyberSource v2.0, it is strongly recommended to create the stored credential profile for the payment method through specifying the &#x60;mitProfileAction&#x60;, &#x60;mitConsentAgreementSrc&#x60;, and &#x60;mitProfileType&#x60; fields to prevent payment rejection.  | [optional] 
**MitProfileAction** | **string** | This field is only available for CyberSource v2.0 integration to create a stored credential profile within the payment method.   When creating an Apple Pay payment method on CyberSource v2.0, it is strongly recommended to create the stored credential profile for the payment method through specifying the &#x60;mitProfileAction&#x60;, &#x60;mitConsentAgreementSrc&#x60;, and &#x60;mitProfileType&#x60; fields to prevent payment rejection.      - &#x60;Activate&#x60; - Use this value if you are creating the stored credential profile after receiving the customer&#39;s consent.        Zuora will create the stored credential profile then send a cardholder-initiated transaction (CIT) to the payment gateway to validate the stored credential profile. If the CIT succeeds, the status of the stored credential profile will be &#x60;Active&#x60;. If the CIT does not succeed, Zuora will not create a stored credential profile.          If the payment gateway does not support the stored credential transaction framework, the status of the stored credential profile will be &#x60;Agreed&#x60;.      - &#x60;Persist&#x60; - Use this value if the stored credential profile represents a stored credential profile in an external system. The status of the payment method&#39;s stored credential profile will be &#x60;Active&#x60;.       | [optional] 
**MitProfileType** | **string** | This field is only available for CyberSource v2.0 integration to create a stored credential profile within the payment method. It is required if the &#x60;mitProfileAction&#x60; field is specified. This field indicates the type of the stored credential profile to process recurring or unsecheduled transactions.  When creating an Apple Pay payment method on CyberSource v2.0, it is strongly recommended to create the stored credential profile for the payment method through specifying the &#x60;mitProfileAction&#x60;, &#x60;mitConsentAgreementSrc&#x60;, and &#x60;mitProfileType&#x60; fields to prevent payment rejection.       | [optional] 
**PaymentGateway** | **string** | The label name of the gateway instance configured in Zuora that should process the payment. When creating a Payment, this must be a valid gateway instance ID and this gateway must support the specific payment method. If not specified, the default gateway of your Zuora customer account will be used.  **Note:** When &#x60;processPayment&#x60; is &#x60;true&#x60;, this field is required. When &#x60;processPayment&#x60; is &#x60;false&#x60;, the default payment gateway of your Zuora customer account will be used no matter whether a payment gateway instance is specified in the &#x60;paymentGateway&#x60; field.  | [optional] 
**PaymentToken** | **Object** | The complete JSON Object representing the encrypted payment token payload returned in the response from the Apple Pay session.   | 
**ProcessPayment** | **bool** | A boolean flag to control whether a payment should be processed after creating payment method. The payment amount will be equivalent to the amount the merchant supplied in the ApplePay session. Default is false.  If this field is set to &#x60;true&#x60;, you must specify the &#x60;paymentGateway&#x60; field with the payment gateway instance name.  If this field is set to &#x60;false&#x60;:   - The default payment gateway of your Zuora customer account will be used no matter whether a payment gateway instance is specified in the &#x60;paymentGateway&#x60; field.    - You must select the **Verify new credit card** check box on the gateway instance settings page. Otherwise, the cryptogram will not be sent to the gateway.   - A separate subscribe or payment API call is required after this payment method creation call.  | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)
