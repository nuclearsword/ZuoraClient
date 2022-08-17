/*
 * API Reference: Billing
 *
 *   # Introduction  Welcome to the reference for the Zuora Billing REST API!  To learn about the common use cases of Zuora Billing REST APIs, check out the [API Guides](https://www.zuora.com/developer/api-guides/).  In addition to Zuora API Reference; Billing, we also provide API references for other Zuora products:    * [API Reference: Collections](https://www.zuora.com/developer/collect-api/)   * [API Reference: Revenue](https://www.zuora.com/developer/revpro-api/)      The Zuora REST API provides a broad set of operations and resources that:    * Enable Web Storefront integration from your website.   * Support self-service subscriber sign-ups and account management.   * Process revenue schedules through custom revenue rule models.   * Enable manipulation of most objects in the Zuora Billing Object Model.  Want to share your opinion on how our API works for you? <a href=\"https://community.zuora.com/t5/Developers/API-Feedback-Form/gpm-p/21399\" target=\"_blank\">Tell us how you feel </a>about using our API and what we can do to make it better.  ## Access to the API  If you have a Zuora tenant, you can access the Zuora REST API via one of the following endpoints:  | Tenant              | Base URL for REST Endpoints | |- -- -- -- -- -- -- -- -- -- -- -- --|- -- -- -- -- -- -- -- -- -- -- -- --| |US Cloud 1 Production | https://rest.na.zuora.com  | |US Cloud 1 API Sandbox |  https://rest.sandbox.na.zuora.com | |US Cloud 2 Production | https://rest.zuora.com | |US Cloud 2 API Sandbox | https://rest.apisandbox.zuora.com| |US Central Sandbox | https://rest.test.zuora.com |   |US Performance Test | https://rest.pt1.zuora.com | |US Production Copy | Submit a request at <a href=\"http://support.zuora.com/\" target=\"_blank\">Zuora Global Support</a> to enable the Zuora REST API in your tenant and obtain the base URL for REST endpoints. See [REST endpoint base URL of Production Copy (Service) Environment for existing and new customers](https://community.zuora.com/t5/API/REST-endpoint-base-URL-of-Production-Copy-Service-Environment/td-p/29611) for more information. | |EU Production | https://rest.eu.zuora.com | |EU API Sandbox | https://rest.sandbox.eu.zuora.com | |EU Central Sandbox | https://rest.test.eu.zuora.com |  The Production endpoint provides access to your live user data. Sandbox tenants are a good place to test code without affecting real-world data. If you would like Zuora to provision a Sandbox tenant for you, contact your Zuora representative for assistance.   If you do not have a Zuora tenant, go to <a href=\"https://www.zuora.com/resource/zuora-test-drive\" target=\"_blank\">https://www.zuora.com/resource/zuora-test-drive</a> and sign up for a Production Test Drive tenant. The tenant comes with seed data, including a sample product catalog.  # API Changelog You can find the <a href=\"https://bit.ly/3JGqyR3\" target=\"_blank\">Changelog</a> of the API Reference: Billing in the Zuora Community.  # Authentication  ## OAuth v2.0  Zuora recommends that you use OAuth v2.0 to authenticate to the Zuora REST API. Currently, OAuth is not available in every environment. See [Zuora Testing Environments](https://knowledgecenter.zuora.com/BB_Introducing_Z_Business/D_Zuora_Environments) for more information.  Zuora recommends you to create a dedicated API user with API write access on a tenant when authenticating via OAuth, and then create an OAuth client for this user. See <a href=\"https://knowledgecenter.zuora.com/CF_Users_and_Administrators/A_Administrator_Settings/Manage_Users/Create_an_API_User\" target=\"_blank\">Create an API User</a> for how to do this. By creating a dedicated API user, you can control permissions of the API user without affecting other non-API users.  If a user is deactivated, all of the user's OAuth clients will be automatically deactivated.  Authenticating via OAuth requires the following steps: 1. Create a Client 2. Generate a Token 3. Make Authenticated Requests  ### Create a Client  You must first [create an OAuth client](https://knowledgecenter.zuora.com/CF_Users_and_Administrators/A_Administrator_Settings/Manage_Users#Create_an_OAuth_Client_for_a_User) in the Zuora UI. To do this, you must be an administrator of your Zuora tenant. This is a one-time operation. You will be provided with a Client ID and a Client Secret. Please note this information down, as it will be required for the next step.  **Note:** The OAuth client will be owned by a Zuora user account. If you want to perform PUT, POST, or DELETE operations using the OAuth client, the owner of the OAuth client must have a Platform role that includes the \"API Write Access\" permission.  ### Generate a Token  After creating a client, you must make a call to obtain a bearer token using the [Generate an OAuth token](https://www.zuora.com/developer/api-reference/#operation/createToken) operation. This operation requires the following parameters: - `client_id` - the Client ID displayed when you created the OAuth client in the previous step - `client_secret` - the Client Secret displayed when you created the OAuth client in the previous step - `grant_type` - must be set to `client_credentials`  **Note**: The Client ID and Client Secret mentioned above were displayed when you created the OAuth Client in the prior step. The [Generate an OAuth token](https://www.zuora.com/developer/api-reference/#operation/createToken) response specifies how long the bearer token is valid for. You should reuse the bearer token until it is expired. When the token is expired, call [Generate an OAuth token](https://www.zuora.com/developer/api-reference/#operation/createToken) again to generate a new one.  ### Make Authenticated Requests  To authenticate subsequent API requests, you must provide a valid bearer token in an HTTP header:  `Authorization: Bearer {bearer_token}`  If you have [Zuora Multi-entity](https://www.zuora.com/developer/api-reference/#tag/Entities) enabled, you need to set an additional header to specify the ID of the entity that you want to access. You can use the `scope` field in the [Generate an OAuth token](https://www.zuora.com/developer/api-reference/#operation/createToken) response to determine whether you need to specify an entity ID.  If the `scope` field contains more than one entity ID, you must specify the ID of the entity that you want to access. For example, if the `scope` field contains `entity.1a2b7a37-3e7d-4cb3-b0e2-883de9e766cc` and `entity.c92ed977-510c-4c48-9b51-8d5e848671e9`, specify one of the following headers: - `Zuora-Entity-Ids: 1a2b7a37-3e7d-4cb3-b0e2-883de9e766cc` - `Zuora-Entity-Ids: c92ed977-510c-4c48-9b51-8d5e848671e9`  **Note**: For a limited period of time, Zuora will accept the `entityId` header as an alternative to the `Zuora-Entity-Ids` header. If you choose to set the `entityId` header, you must remove all \"-\" characters from the entity ID in the `scope` field.  If the `scope` field contains a single entity ID, you do not need to specify an entity ID.  ## Other Supported Authentication Schemes  Zuora continues to support the following additional legacy means of authentication:    * Use username and password. Include authentication with each request in the header:         * `apiAccessKeyId`      * `apiSecretAccessKey`          Zuora recommends that you create an API user specifically for making API calls. See <a href=\"https://knowledgecenter.zuora.com/CF_Users_and_Administrators/A_Administrator_Settings/Manage_Users/Create_an_API_User\" target=\"_blank\">Create an API User</a> for more information.      * Use an authorization cookie. The cookie authorizes the user to make calls to the REST API for the duration specified in  **Administration > Security Policies > Session timeout**. The cookie expiration time is reset with this duration after every call to the REST API. To obtain a cookie, call the [Connections](https://www.zuora.com/developer/api-reference/#tag/Connections) resource with the following API user information:         *   ID         *   Password        * For CORS-enabled APIs only: Include a 'single-use' token in the request header, which re-authenticates the user with each request. See below for more details.  ### Entity Id and Entity Name  The `entityId` and `entityName` parameters are only used for [Zuora Multi-entity](https://knowledgecenter.zuora.com/BB_Introducing_Z_Business/Multi-entity \"Zuora Multi-entity\"). These are the legacy parameters that Zuora will only continue to support for a period of time. Zuora recommends you to use the `Zuora-Entity-Ids` parameter instead.   The  `entityId` and `entityName` parameters specify the Id and the [name of the entity](https://knowledgecenter.zuora.com/BB_Introducing_Z_Business/Multi-entity/B_Introduction_to_Entity_and_Entity_Hierarchy#Name_and_Display_Name \"Introduction to Entity and Entity Hierarchy\") that you want to access, respectively. Note that you must have permission to access the entity.   You can specify either the `entityId` or `entityName` parameter in the authentication to access and view an entity.    * If both `entityId` and `entityName` are specified in the authentication, an error occurs.    * If neither `entityId` nor `entityName` is specified in the authentication, you will log in to the entity in which your user account is created.      To get the entity Id and entity name, you can use the GET Entities REST call. For more information, see [API User Authentication](https://knowledgecenter.zuora.com/BB_Introducing_Z_Business/Multi-entity/A_Overview_of_Multi-entity#API_User_Authentication \"API User Authentication\").      ### Token Authentication for CORS-Enabled APIs      The CORS mechanism enables REST API calls to Zuora to be made directly from your customer's browser, with all credit card and security information transmitted directly to Zuora. This minimizes your PCI compliance burden, allows you to implement advanced validation on your payment forms, and  makes your payment forms look just like any other part of your website.    For security reasons, instead of using cookies, an API request via CORS uses **tokens** for authentication.  The token method of authentication is only designed for use with requests that must originate from your customer's browser; **it should  not be considered a replacement to the existing cookie authentication** mechanism.  See [Zuora CORS REST](https://knowledgecenter.zuora.com/DC_Developers/C_REST_API/Zuora_CORS_REST \"Zuora CORS REST\") for details on how CORS works and how you can begin to implement customer calls to the Zuora REST APIs. See  [HMAC Signatures](https://www.zuora.com/developer/api-reference/#operation/POSTHMACSignature \"HMAC Signatures\") for details on the HMAC method that returns the authentication token.  # Requests and Responses  ## Request IDs  As a general rule, when asked to supply a \"key\" for an account or subscription (accountKey, account-key, subscriptionKey, subscription-key), you can provide either the actual ID or  the number of the entity.  ## HTTP Request Body  Most of the parameters and data accompanying your requests will be contained in the body of the HTTP request.   The Zuora REST API accepts JSON in the HTTP request body. No other data format (e.g., XML) is supported.  ### Data Type  ([Actions](https://www.zuora.com/developer/api-reference/#tag/Actions) and CRUD operations only) We recommend that you do not specify the decimal values with quotation marks, commas, and spaces. Use characters of `+-0-9.eE`, for example, `5`, `1.9`, `-8.469`, and `7.7e2`. Also, Zuora does not convert currencies for decimal values.   ## Making asynchronous requests  Most Zuora REST API endpoints documented in this API Reference process requests synchronously. In high-throughput scenarios, your requests to these endpoints are usually rate limited.   One strategy for avoiding rate limits is to make asynchronous requests, and Zuora provides this option to you.  Making asynchronous requests allows you to scale your applications more efficiently by leveraging Zuora's infrastructure to enqueue and execute requests for you without blocking. These requests also use built-in retry semantics, which makes them much less likely to fail for non-deterministic reasons, even in extreme high-throughput scenarios.  Meanwhile, when you send a request to one of these endpoints, you can receive a response in less than 150 milliseconds and these calls are unlikely to trigger rate limit errors.   You can make asynchronous requests to the POST, PUT, or DELETE operations, except [Actions](https://www.zuora.com/developer/api-reference/#tag/Actions), for all resources documented in this API Reference.  Take the following steps to take advantage of the asynchronous API:    1. Set up callout notification   2. Make asynchronous requests  The following sections describes the high-level steps for you to get the most of the asynchronous API. For detailed instructions, see [Make asynchronous requests](https://knowledgecenter.zuora.com/Central_Platform/API/AA_REST_API/Make_asynchronous_requests) in the Knowledge Center.   ### Set up notifications  You can create callout notification definitions based on the following custom events through the Zuora UI or the [Create a notification definition](https://www.zuora.com/developer/api-reference/#operation/POST_Create_Notification_Definition) API operation:   * Async Request Succeeded   * Async Request Failed  This step ensures that your system receives a callout when an asynchronous request succeeds or fails.   ### Make asynchronous requests  By design, asynchronous requests differ from their synchronous counterparts in endpoints, and the HTTP status response code and response body they return. ​​The header parameters and request body schema for asynchronous operations are the same as their synchronous counterparts.   * The endpoints for asynchronous API operations contain `/async` in the path after `/v1`. See the following table for examples:  | Operation               | Synchronous endpoint  | Asynchronous endpoint      | |:- -- -- -- - |:- -- -- -- - |:- -- -- -- - | | Create an account       | `/v1/accounts`        | `/v1/async/accounts`       | | CRUD: Create an account | `/v1/object/account`  | `/v1/async/object/account` |  * Unlike the 200 OK response for synchronous requests, Zuora returns a 202 Accepted response for all asynchronous requests, and the response body contains only a request ID.   **Note**: These asynchronous API endpoints are in addition to the previously introduced endpoints that support asynchronous processing. You should continue to use them:   * [Perform a mass action](https://www.zuora.com/developer/api-reference/#operation/POST_MassUpdater)   * [Create an order asynchronously](https://www.zuora.com/developer/api-reference/#operation/POST_CreateOrderAsynchronously)   * [Preview an order asynchronously](https://www.zuora.com/developer/api-reference/#operation/POST_PreviewOrderAsynchronously)   * [Create a job to hard delete billing document files](https://www.zuora.com/developer/api-reference/#operation/POST_BillingDocumentFilesDeletionJob)   * [CRUD: Post or cancel a bill run](https://www.zuora.com/developer/api-reference/#operation/Object_PUTBillRun)   * [Cancel a journal run](https://www.zuora.com/developer/api-reference/#operation/PUT_JournalRun)   * [Run trial balance](https://www.zuora.com/developer/api-reference/#operation/PUT_RunTrialBalance)  For more information, see [Make asynchronous requests](https://knowledgecenter.zuora.com/Central_Platform/API/AA_REST_API/Make_asynchronous_requests).  ## Testing a Request  Use a third party client, such as [curl](https://curl.haxx.se \"curl\"), [Postman](https://www.getpostman.com \"Postman\"), or [Advanced REST Client](https://advancedrestclient.com \"Advanced REST Client\"), to test the Zuora REST API.  You can test the Zuora REST API from the Zuora API Sandbox or Production tenants. If connecting to Production, bear in mind that you are working with your live production data, not sample data or test data.  ## Testing with Credit Cards  Sooner or later it will probably be necessary to test some transactions that involve credit cards. For suggestions on how to handle this, see [Going Live With Your Payment Gateway](https://knowledgecenter.zuora.com/CB_Billing/M_Payment_Gateways/C_Managing_Payment_Gateways/B_Going_Live_Payment_Gateways#Testing_with_Credit_Cards \"C_Zuora_User_Guides/A_Billing_and_Payments/M_Payment_Gateways/C_Managing_Payment_Gateways/B_Going_Live_Payment_Gateways#Testing_with_Credit_Cards\" ).  ## Concurrent Request Limits  Zuora enforces tenant-level concurrent request limits. See <a href=\"https://knowledgecenter.zuora.com/BB_Introducing_Z_Business/Policies/Concurrent_Request_Limits\" target=\"_blank\">Concurrent Request Limits</a> for more information.  ## Timeout Limit  If a request does not complete within 120 seconds, the request times out and Zuora returns a Gateway Timeout error.   # Error Handling  If a request to Zuora Billing REST API with an endpoint starting with `/v1` (except [Actions](https://www.zuora.com/developer/api-reference/#tag/Actions) and CRUD operations) fails, the response will contain an eight-digit error code with a corresponding error message to indicate the details of the error.  The following code snippet is a sample error response that contains an error code and message pair:  ```  {    \"success\": false,    \"processId\": \"CBCFED6580B4E076\",    \"reasons\":  [      {       \"code\": 53100320,       \"message\": \"'termType' value should be one of: TERMED, EVERGREEN\"      }     ]  } ``` The `success` field indicates whether the API request has succeeded. The `processId` field is a Zuora internal ID that you can provide to Zuora Global Support for troubleshooting purposes.  The `reasons` field contains the actual error code and message pair. The error code begins with `5` or `6` means that you encountered a certain issue that is specific to a REST API resource in Zuora Billing. For example, `53100320` indicates that an invalid value is specified for the `termType` field of the `subscription` object.  The error code beginning with `9` usually indicates that an authentication-related issue occurred, and it can also indicate other unexpected errors depending on different cases. For example, `90000011` indicates that an invalid credential is provided in the request header.   When troubleshooting the error, you can divide the error code into two components: REST API resource code and error category code. See the following Zuora error code sample:  <a href=\"https://assets.zuora.com/zuora-documentation/ZuoraErrorCode.jpeg\" target=\"_blank\"><img src=\"https://assets.zuora.com/zuora-documentation/ZuoraErrorCode.jpeg\" alt=\"Zuora Error Code Sample\"></a>   **Note:** Zuora determines resource codes based on the request payload. Therefore, if GET and DELETE requests that do not contain payloads fail, you will get `500000` as the resource code, which indicates an unknown object and an unknown field.  The error category code of these requests is valid and follows the rules described in the [Error Category Code](https://www.zuora.com/developer/api-reference/#section/Error-Handling/Error-Category-Code) section.  In such case, you can refer to the returned error message to troubleshoot.   ## REST API Resource Codes  The 6-digit resource code indicates the REST API resource, typically a field of a Zuora object, on which the issue occurs. In the preceding example, `531003` refers to the `termType` field of the `subscription` object.   The value range for all REST API resource codes is from `500000` to `679999`. See [Resource Codes](https://knowledgecenter.zuora.com/Central_Platform/API/AA_REST_API/Resource_Codes) in the Knowledge Center for a full list of resource codes.  ## Error Category Codes  The 2-digit error category code identifies the type of error, for example, resource not found or missing required field.   The following table describes all error categories and the corresponding resolution:  | Code    | Error category              | Description    | Resolution    | |:- -- -- -- -|:- -- -- -- -|:- -- -- -- -|:- -- -- -- -| | 10      | Permission or access denied | The request cannot be processed because a certain tenant or user permission is missing. | Check the missing tenant or user permission in the response message and contact [Zuora Global Support](https://support.zuora.com) for enablement. | | 11      | Authentication failed       | Authentication fails due to invalid API authentication credentials. | Ensure that a valid API credential is specified. | | 20      | Invalid format or value     | The request cannot be processed due to an invalid field format or value. | Check the invalid field in the error message, and ensure that the format and value of all fields you passed in are valid. | | 21      | Unknown field in request    | The request cannot be processed because an unknown field exists in the request body. | Check the unknown field name in the response message, and ensure that you do not include any unknown field in the request body. | | 22      | Missing required field      | The request cannot be processed because a required field in the request body is missing. | Check the missing field name in the response message, and ensure that you include all required fields in the request body. | | 23      | Missing required parameter  | The request cannot be processed because a required query parameter is missing. | Check the missing parameter name in the response message, and ensure that you include the parameter in the query. | | 30      | Rule restriction            | The request cannot be processed due to the violation of a Zuora business rule. | Check the response message and ensure that the API request meets the specified business rules. | | 40      | Not found                   | The specified resource cannot be found. | Check the response message and ensure that the specified resource exists in your Zuora tenant. | | 45      | Unsupported request         | The requested endpoint does not support the specified HTTP method. | Check your request and ensure that the endpoint and method matches. | | 50      | Locking contention          | This request cannot be processed because the objects this request is trying to modify are being modified by another API request, UI operation, or batch job process. | <p>Resubmit the request first to have another try.</p> <p>If this error still occurs, contact [Zuora Global Support](https://support.zuora.com) with the returned `Zuora-Request-Id` value in the response header for assistance.</p> | | 60      | Internal error              | The server encounters an internal error. | Contact [Zuora Global Support](https://support.zuora.com) with the returned `Zuora-Request-Id` value in the response header for assistance. | | 61      | Temporary error             | A temporary error occurs during request processing, for example, a database communication error. | <p>Resubmit the request first to have another try.</p> <p>If this error still occurs, contact [Zuora Global Support](https://support.zuora.com) with the returned `Zuora-Request-Id` value in the response header for assistance. </p>| | 70      | Request exceeded limit      | The total number of concurrent requests exceeds the limit allowed by the system. | <p>Resubmit the request after the number of seconds specified by the `Retry-After` value in the response header.</p> <p>Check [Concurrent request limits](https://knowledgecenter.zuora.com/BB_Introducing_Z_Business/Policies/Concurrent_Request_Limits) for details about Zuora’s concurrent request limit policy.</p> | | 90      | Malformed request           | The request cannot be processed due to JSON syntax errors. | Check the syntax error in the JSON request body and ensure that the request is in the correct JSON format. | | 99      | Integration error           | The server encounters an error when communicating with an external system, for example, payment gateway, tax engine provider. | Check the response message and take action accordingly. |   # Pagination  When retrieving information (using GET methods), the optional `pageSize` query parameter sets the maximum number of rows to return in a response. The maximum is `40`; larger values are treated as `40`. If this value is empty or invalid, `pageSize` typically defaults to `10`.  The default value for the maximum number of rows retrieved can be overridden at the method level.  If more rows are available, the response will include a `nextPage` element, which contains a URL for requesting the next page.  If this value is not provided, no more rows are available. No \"previous page\" element is explicitly provided; to support backward paging, use the previous call.  ## Array Size  For data items that are not paginated, the REST API supports arrays of up to 300 rows.  Thus, for instance, repeated pagination can retrieve thousands of customer accounts, but within any account an array of no more than 300 rate plans is returned.  # API Versions  The Zuora REST API are version controlled. Versioning ensures that Zuora REST API changes are backward compatible. Zuora uses a major and minor version nomenclature to manage changes. By specifying a version in a REST request, you can get expected responses regardless of future changes to the API.  ## Major Version  The major version number of the REST API appears in the REST URL. Currently, Zuora only supports the **v1** major version. For example, `POST https://rest.zuora.com/v1/subscriptions`.  ## Minor Version  Zuora uses minor versions for the REST API to control small changes. For example, a field in a REST method is deprecated and a new field is used to replace it.   Some fields in the REST methods are supported as of minor versions. If a field is not noted with a minor version, this field is available for all minor versions. If a field is noted with a minor version, this field is in version control. You must specify the supported minor version in the request header to process without an error.   If a field is in version control, it is either with a minimum minor version or a maximum minor version, or both of them. You can only use this field with the minor version between the minimum and the maximum minor versions. For example, the `invoiceCollect` field in the POST Subscription method is in version control and its maximum minor version is 189.0. You can only use this field with the minor version 189.0 or earlier.  If you specify a version number in the request header that is not supported, Zuora will use the minimum minor version of the REST API. In our REST API documentation, if a field or feature requires a minor version number, we note that in the field description.  You only need to specify the version number when you use the fields require a minor version. To specify the minor version, set the `zuora-version` parameter to the minor version number in the request header for the request call. For example, the `collect` field is in 196.0 minor version. If you want to use this field for the POST Subscription method, set the  `zuora-version` parameter to `196.0` in the request header. The `zuora-version` parameter is case sensitive.  For all the REST API fields, by default, if the minor version is not specified in the request header, Zuora will use the minimum minor version of the REST API to avoid breaking your integration.   ### Minor Version History  The supported minor versions are not serial. This section documents the changes made to each Zuora REST API minor version.  The following table lists the supported versions and the fields that have a Zuora REST API minor version.  | Fields         | Minor Version      | REST Methods    | Description | |:- -- -- -- -|:- -- -- -- -|:- -- -- -- -|:- -- -- -- -| | invoiceCollect | 189.0 and earlier  | [Create Subscription](https://www.zuora.com/developer/api-reference/#operation/POST_Subscription \"Create Subscription\"); [Update Subscription](https://www.zuora.com/developer/api-reference/#operation/PUT_Subscription \"Update Subscription\"); [Renew Subscription](https://www.zuora.com/developer/api-reference/#operation/PUT_RenewSubscription \"Renew Subscription\"); [Cancel Subscription](https://www.zuora.com/developer/api-reference/#operation/PUT_CancelSubscription \"Cancel Subscription\"); [Suspend Subscription](https://www.zuora.com/developer/api-reference/#operation/PUT_SuspendSubscription \"Suspend Subscription\"); [Resume Subscription](https://www.zuora.com/developer/api-reference/#operation/PUT_ResumeSubscription \"Resume Subscription\"); [Create Account](https://www.zuora.com/developer/api-reference/#operation/POST_Account \"Create Account\")|Generates an invoice and collects a payment for a subscription. | | collect        | 196.0 and later    | [Create Subscription](https://www.zuora.com/developer/api-reference/#operation/POST_Subscription \"Create Subscription\"); [Update Subscription](https://www.zuora.com/developer/api-reference/#operation/PUT_Subscription \"Update Subscription\"); [Renew Subscription](https://www.zuora.com/developer/api-reference/#operation/PUT_RenewSubscription \"Renew Subscription\"); [Cancel Subscription](https://www.zuora.com/developer/api-reference/#operation/PUT_CancelSubscription \"Cancel Subscription\"); [Suspend Subscription](https://www.zuora.com/developer/api-reference/#operation/PUT_SuspendSubscription \"Suspend Subscription\"); [Resume Subscription](https://www.zuora.com/developer/api-reference/#operation/PUT_ResumeSubscription \"Resume Subscription\"); [Create Account](https://www.zuora.com/developer/api-reference/#operation/POST_Account \"Create Account\")|Collects an automatic payment for a subscription. | | invoice | 196.0 and 207.0| [Create Subscription](https://www.zuora.com/developer/api-reference/#operation/POST_Subscription \"Create Subscription\"); [Update Subscription](https://www.zuora.com/developer/api-reference/#operation/PUT_Subscription \"Update Subscription\"); [Renew Subscription](https://www.zuora.com/developer/api-reference/#operation/PUT_RenewSubscription \"Renew Subscription\"); [Cancel Subscription](https://www.zuora.com/developer/api-reference/#operation/PUT_CancelSubscription \"Cancel Subscription\"); [Suspend Subscription](https://www.zuora.com/developer/api-reference/#operation/PUT_SuspendSubscription \"Suspend Subscription\"); [Resume Subscription](https://www.zuora.com/developer/api-reference/#operation/PUT_ResumeSubscription \"Resume Subscription\"); [Create Account](https://www.zuora.com/developer/api-reference/#operation/POST_Account \"Create Account\")|Generates an invoice for a subscription. | | invoiceTargetDate | 196.0 and earlier  | [Preview Subscription](https://www.zuora.com/developer/api-reference/#operation/POST_SubscriptionPreview \"Preview Subscription\") |Date through which charges are calculated on the invoice, as `yyyy-mm-dd`. | | invoiceTargetDate | 207.0 and earlier  | [Create Subscription](https://www.zuora.com/developer/api-reference/#operation/POST_Subscription \"Create Subscription\"); [Update Subscription](https://www.zuora.com/developer/api-reference/#operation/PUT_Subscription \"Update Subscription\"); [Renew Subscription](https://www.zuora.com/developer/api-reference/#operation/PUT_RenewSubscription \"Renew Subscription\"); [Cancel Subscription](https://www.zuora.com/developer/api-reference/#operation/PUT_CancelSubscription \"Cancel Subscription\"); [Suspend Subscription](https://www.zuora.com/developer/api-reference/#operation/PUT_SuspendSubscription \"Suspend Subscription\"); [Resume Subscription](https://www.zuora.com/developer/api-reference/#operation/PUT_ResumeSubscription \"Resume Subscription\"); [Create Account](https://www.zuora.com/developer/api-reference/#operation/POST_Account \"Create Account\")|Date through which charges are calculated on the invoice, as `yyyy-mm-dd`. | | targetDate | 207.0 and later | [Preview Subscription](https://www.zuora.com/developer/api-reference/#operation/POST_SubscriptionPreview \"Preview Subscription\") |Date through which charges are calculated on the invoice, as `yyyy-mm-dd`. | | targetDate | 211.0 and later | [Create Subscription](https://www.zuora.com/developer/api-reference/#operation/POST_Subscription \"Create Subscription\"); [Update Subscription](https://www.zuora.com/developer/api-reference/#operation/PUT_Subscription \"Update Subscription\"); [Renew Subscription](https://www.zuora.com/developer/api-reference/#operation/PUT_RenewSubscription \"Renew Subscription\"); [Cancel Subscription](https://www.zuora.com/developer/api-reference/#operation/PUT_CancelSubscription \"Cancel Subscription\"); [Suspend Subscription](https://www.zuora.com/developer/api-reference/#operation/PUT_SuspendSubscription \"Suspend Subscription\"); [Resume Subscription](https://www.zuora.com/developer/api-reference/#operation/PUT_ResumeSubscription \"Resume Subscription\"); [Create Account](https://www.zuora.com/developer/api-reference/#operation/POST_Account \"Create Account\")|Date through which charges are calculated on the invoice, as `yyyy-mm-dd`. | | includeExisting DraftInvoiceItems | 196.0 and earlier| [Preview Subscription](https://www.zuora.com/developer/api-reference/#operation/POST_SubscriptionPreview \"Preview Subscription\"); [Update Subscription](https://www.zuora.com/developer/api-reference/#operation/PUT_Subscription \"Update Subscription\") | Specifies whether to include draft invoice items in subscription previews. Specify it to be `true` (default) to include draft invoice items in the preview result. Specify it to be `false` to excludes draft invoice items in the preview result. | | includeExisting DraftDocItems | 207.0 and later  | [Preview Subscription](https://www.zuora.com/developer/api-reference/#operation/POST_SubscriptionPreview \"Preview Subscription\"); [Update Subscription](https://www.zuora.com/developer/api-reference/#operation/PUT_Subscription \"Update Subscription\") | Specifies whether to include draft invoice items in subscription previews. Specify it to be `true` (default) to include draft invoice items in the preview result. Specify it to be `false` to excludes draft invoice items in the preview result. | | previewType | 196.0 and earlier| [Preview Subscription](https://www.zuora.com/developer/api-reference/#operation/POST_SubscriptionPreview \"Preview Subscription\"); [Update Subscription](https://www.zuora.com/developer/api-reference/#operation/PUT_Subscription \"Update Subscription\") | The type of preview you will receive. The possible values are `InvoiceItem`(default), `ChargeMetrics`, and `InvoiceItemChargeMetrics`. | | previewType | 207.0 and later  | [Preview Subscription](https://www.zuora.com/developer/api-reference/#operation/POST_SubscriptionPreview \"Preview Subscription\"); [Update Subscription](https://www.zuora.com/developer/api-reference/#operation/PUT_Subscription \"Update Subscription\") | The type of preview you will receive. The possible values are `LegalDoc`(default), `ChargeMetrics`, and `LegalDocChargeMetrics`. | | runBilling  | 211.0 and later  | [Create Subscription](https://www.zuora.com/developer/api-reference/#operation/POST_Subscription \"Create Subscription\"); [Update Subscription](https://www.zuora.com/developer/api-reference/#operation/PUT_Subscription \"Update Subscription\"); [Renew Subscription](https://www.zuora.com/developer/api-reference/#operation/PUT_RenewSubscription \"Renew Subscription\"); [Cancel Subscription](https://www.zuora.com/developer/api-reference/#operation/PUT_CancelSubscription \"Cancel Subscription\"); [Suspend Subscription](https://www.zuora.com/developer/api-reference/#operation/PUT_SuspendSubscription \"Suspend Subscription\"); [Resume Subscription](https://www.zuora.com/developer/api-reference/#operation/PUT_ResumeSubscription \"Resume Subscription\"); [Create Account](https://www.zuora.com/developer/api-reference/#operation/POST_Account \"Create Account\")|Generates an invoice or credit memo for a subscription. **Note:** Credit memos are only available if you have the Invoice Settlement feature enabled. | | invoiceDate | 214.0 and earlier  | [Invoice and Collect](https://www.zuora.com/developer/api-reference/#operation/POST_TransactionInvoicePayment \"Invoice and Collect\") |Date that should appear on the invoice being generated, as `yyyy-mm-dd`. | | invoiceTargetDate | 214.0 and earlier  | [Invoice and Collect](https://www.zuora.com/developer/api-reference/#operation/POST_TransactionInvoicePayment \"Invoice and Collect\") |Date through which to calculate charges on this account if an invoice is generated, as `yyyy-mm-dd`. | | documentDate | 215.0 and later | [Invoice and Collect](https://www.zuora.com/developer/api-reference/#operation/POST_TransactionInvoicePayment \"Invoice and Collect\") |Date that should appear on the invoice and credit memo being generated, as `yyyy-mm-dd`. | | targetDate | 215.0 and later | [Invoice and Collect](https://www.zuora.com/developer/api-reference/#operation/POST_TransactionInvoicePayment \"Invoice and Collect\") |Date through which to calculate charges on this account if an invoice or a credit memo is generated, as `yyyy-mm-dd`. | | memoItemAmount | 223.0 and earlier | [Create credit memo from charge](https://www.zuora.com/developer/api-reference/#operation/POST_CreditMemoFromPrpc \"Create credit memo from charge\"); [Create debit memo from charge](https://www.zuora.com/developer/api-reference/#operation/POST_DebitMemoFromPrpc \"Create debit memo from charge\") | Amount of the memo item. | | amount | 224.0 and later | [Create credit memo from charge](https://www.zuora.com/developer/api-reference/#operation/POST_CreditMemoFromPrpc \"Create credit memo from charge\"); [Create debit memo from charge](https://www.zuora.com/developer/api-reference/#operation/POST_DebitMemoFromPrpc \"Create debit memo from charge\") | Amount of the memo item. | | subscriptionNumbers | 222.4 and earlier | [Create order](https://www.zuora.com/developer/api-reference/#operation/POST_Order \"Create order\") | Container for the subscription numbers of the subscriptions in an order. | | subscriptions | 223.0 and later | [Create order](https://www.zuora.com/developer/api-reference/#operation/POST_Order \"Create order\") | Container for the subscription numbers and statuses in an order. | | creditTaxItems | 238.0 and earlier | [Get credit memo items](https://www.zuora.com/developer/api-reference/#operation/GET_CreditMemoItems \"Get credit memo items\"); [Get credit memo item](https://www.zuora.com/developer/api-reference/#operation/GET_CreditMemoItem \"Get credit memo item\") | Container for the taxation items of the credit memo item. | | taxItems | 238.0 and earlier | [Get debit memo items](https://www.zuora.com/developer/api-reference/#operation/GET_DebitMemoItems \"Get debit memo items\"); [Get debit memo item](https://www.zuora.com/developer/api-reference/#operation/GET_DebitMemoItem \"Get debit memo item\") | Container for the taxation items of the debit memo item. | | taxationItems | 239.0 and later | [Get credit memo items](https://www.zuora.com/developer/api-reference/#operation/GET_CreditMemoItems \"Get credit memo items\"); [Get credit memo item](https://www.zuora.com/developer/api-reference/#operation/GET_CreditMemoItem \"Get credit memo item\"); [Get debit memo items](https://www.zuora.com/developer/api-reference/#operation/GET_DebitMemoItems \"Get debit memo items\"); [Get debit memo item](https://www.zuora.com/developer/api-reference/#operation/GET_DebitMemoItem \"Get debit memo item\") | Container for the taxation items of the memo item. | | chargeId | 256.0 and earlier | [Create credit memo from charge](https://www.zuora.com/developer/api-reference/#operation/POST_CreditMemoFromPrpc \"Create credit memo from charge\"); [Create debit memo from charge](https://www.zuora.com/developer/api-reference/#operation/POST_DebitMemoFromPrpc \"Create debit memo from charge\") | ID of the product rate plan charge that the memo is created from. | | productRatePlanChargeId | 257.0 and later | [Create credit memo from charge](https://www.zuora.com/developer/api-reference/#operation/POST_CreditMemoFromPrpc \"Create credit memo from charge\"); [Create debit memo from charge](https://www.zuora.com/developer/api-reference/#operation/POST_DebitMemoFromPrpc \"Create debit memo from charge\") | ID of the product rate plan charge that the memo is created from. | | comment | 256.0 and earlier | [Create credit memo from charge](https://www.zuora.com/developer/api-reference/#operation/POST_CreditMemoFromPrpc \"Create credit memo from charge\"); [Create debit memo from charge](https://www.zuora.com/developer/api-reference/#operation/POST_DebitMemoFromPrpc \"Create debit memo from charge\"); [Create credit memo from invoice](https://www.zuora.com/developer/api-reference/#operation/POST_CreditMemoFromInvoice \"Create credit memo from invoice\"); [Create debit memo from invoice](https://www.zuora.com/developer/api-reference/#operation/POST_DebitMemoFromInvoice \"Create debit memo from invoice\"); [Get credit memo items](https://www.zuora.com/developer/api-reference/#operation/GET_CreditMemoItems \"Get credit memo items\"); [Get credit memo item](https://www.zuora.com/developer/api-reference/#operation/GET_CreditMemoItem \"Get credit memo item\"); [Get debit memo items](https://www.zuora.com/developer/api-reference/#operation/GET_DebitMemoItems \"Get debit memo items\"); [Get debit memo item](https://www.zuora.com/developer/api-reference/#operation/GET_DebitMemoItem \"Get debit memo item\") | Comments about the product rate plan charge, invoice item, or memo item. | | description | 257.0 and later | [Create credit memo from charge](https://www.zuora.com/developer/api-reference/#operation/POST_CreditMemoFromPrpc \"Create credit memo from charge\"); [Create debit memo from charge](https://www.zuora.com/developer/api-reference/#operation/POST_DebitMemoFromPrpc \"Create debit memo from charge\"); [Create credit memo from invoice](https://www.zuora.com/developer/api-reference/#operation/POST_CreditMemoFromInvoice \"Create credit memo from invoice\"); [Create debit memo from invoice](https://www.zuora.com/developer/api-reference/#operation/POST_DebitMemoFromInvoice \"Create debit memo from invoice\"); [Get credit memo items](https://www.zuora.com/developer/api-reference/#operation/GET_CreditMemoItems \"Get credit memo items\"); [Get credit memo item](https://www.zuora.com/developer/api-reference/#operation/GET_CreditMemoItem \"Get credit memo item\"); [Get debit memo items](https://www.zuora.com/developer/api-reference/#operation/GET_DebitMemoItems \"Get debit memo items\"); [Get debit memo item](https://www.zuora.com/developer/api-reference/#operation/GET_DebitMemoItem \"Get debit memo item\") | Description of the the product rate plan charge, invoice item, or memo item. | | taxationItems | 309.0 and later | [Preview an order](https://www.zuora.com/developer/api-reference/#operation/POST_PreviewOrder \"Preview an order\") | List of taxation items for an invoice item or a credit memo item. | | batch | 309.0 and earlier | [Create a billing preview run](https://www.zuora.com/developer/api-reference/#operation/POST_BillingPreviewRun \"Create a billing preview run\") | The customer batches to include in the billing preview run. |       | batches | 314.0 and later | [Create a billing preview run](https://www.zuora.com/developer/api-reference/#operation/POST_BillingPreviewRun \"Create a billing preview run\") | The customer batches to include in the billing preview run. | | taxationItems | 315.0 and later | [Preview a subscription](https://www.zuora.com/developer/api-reference/#operation/POST_PreviewSubscription \"Preview a subscription\"); [Update a subscription](https://www.zuora.com/developer/api-reference/#operation/PUT_Subscription \"Update a subscription\")| List of taxation items for an invoice item or a credit memo item. |    #### Version 207.0 and Later  The response structure of the [Preview Subscription](https://www.zuora.com/developer/api-reference/#operation/POST_SubscriptionPreview \"Preview Subscription\") and [Update Subscription](https://www.zuora.com/developer/api-reference/#operation/PUT_Subscription \"Update Subscription\") methods are changed. The following invoice related response fields are moved to the invoice container:    * amount   * amountWithoutTax   * taxAmount   * invoiceItems   * targetDate   * chargeMetrics  # Zuora Billing Object Model  The following diagram is a high-level view of how key business objects are related to one another within Zuora Billing.  Click the diagram to open it in a new tab and zoom in. For more information about the different sections of the diagram, see <a href=\"https://knowledgecenter.zuora.com/BB_Introducing_Z_Business/A_Zuora_Billing_business_object_model\" target=\"_blank\">Zuora Billing business object model</a>.  <a href=\"https://assets.zuora.com/zuora-documentation/Zuora_Billing_object_model_Sep2020.png\" target=\"_blank\"><img src=\"https://assets.zuora.com/zuora-documentation/Zuora_Billing_object_model_Sep2020.png\" alt=\"Zuora Billing object model diagram\"></a>  This diagram is intended to provide a conceptual understanding; it does not illustrate a specific way to integrate with Zuora.  The diagram includes the Orders feature and the Invoice Settlement feature. If your organization does not use either of these features, see <a href=\"https://knowledgecenter.zuora.com/BB_Introducing_Z_Business/B_Zuora_Billing_business_object_model_prior_to_Orders_and_Invoice_Settlement\" target=\"_blank\">Zuora Billing business object model prior to Orders and Invoice Settlement</a> for an alternative diagram.  ## API Names  You can use the [Describe object](https://www.zuora.com/developer/api-reference/#operation/GET_Describe) operation to list the fields of each Zuora object that is available in your tenant. When you call the operation, you must specify the API name of the Zuora object.  The following table provides the API name of each Zuora object:  | Object                                        | API Name                                   | |- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- --|- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -| | Account                                       | `Account`                                  | | Accounting Code                               | `AccountingCode`                           | | Accounting Period                             | `AccountingPeriod`                         | | Amendment                                     | `Amendment`                                | | Application Group                             | `ApplicationGroup`                         | | Billing Run                                   | <p>`BillingRun` - API name used  in the [Describe object](https://www.zuora.com/developer/api-reference/#operation/GET_Describe) operation, Export ZOQL queries, and Data Query.</p> <p>`BillRun` - API name used in the [Actions](https://www.zuora.com/developer/api-reference/#tag/Actions). See the CRUD oprations of [Bill Run](https://www.zuora.com/developer/api-reference/#tag/Bill-Run) for more information about the `BillRun` object. `BillingRun` and `BillRun` have different fields. |                      | Contact                                       | `Contact`                                  | | Contact Snapshot                              | `ContactSnapshot`                          | | Credit Balance Adjustment                     | `CreditBalanceAdjustment`                  | | Credit Memo                                   | `CreditMemo`                               | | Credit Memo Application                       | `CreditMemoApplication`                    | | Credit Memo Application Item                  | `CreditMemoApplicationItem`                | | Credit Memo Item                              | `CreditMemoItem`                           | | Credit Memo Part                              | `CreditMemoPart`                           | | Credit Memo Part Item                         | `CreditMemoPartItem`                       | | Credit Taxation Item                          | `CreditTaxationItem`                       | | Custom Exchange Rate                          | `FXCustomRate`                             | | Debit Memo                                    | `DebitMemo`                                | | Debit Memo Item                               | `DebitMemoItem`                            | | Debit Taxation Item                           | `DebitTaxationItem`                        | | Discount Applied Metrics                      | `DiscountAppliedMetrics`                   | | Entity                                        | `Tenant`                                   | | Feature                                       | `Feature`                                  | | Gateway Reconciliation Event                  | `PaymentGatewayReconciliationEventLog`     | | Gateway Reconciliation Job                    | `PaymentReconciliationJob`                 | | Gateway Reconciliation Log                    | `PaymentReconciliationLog`                 | | Invoice                                       | `Invoice`                                  | | Invoice Adjustment                            | `InvoiceAdjustment`                        | | Invoice Item                                  | `InvoiceItem`                              | | Invoice Item Adjustment                       | `InvoiceItemAdjustment`                    | | Invoice Payment                               | `InvoicePayment`                           | | Journal Entry                                 | `JournalEntry`                             | | Journal Entry Item                            | `JournalEntryItem`                         | | Journal Run                                   | `JournalRun`                               | | Notification History - Callout                | `CalloutHistory`                           | | Notification History - Email                  | `EmailHistory`                             | | Order                                         | `Order`                                    | | Order Action                                  | `OrderAction`                              | | Order ELP                                     | `OrderElp`                                 | | Order Line Items                              | `OrderLineItems`                           |     | Order Item                                    | `OrderItem`                                | | Order MRR                                     | `OrderMrr`                                 | | Order Quantity                                | `OrderQuantity`                            | | Order TCB                                     | `OrderTcb`                                 | | Order TCV                                     | `OrderTcv`                                 | | Payment                                       | `Payment`                                  | | Payment Application                           | `PaymentApplication`                       | | Payment Application Item                      | `PaymentApplicationItem`                   | | Payment Method                                | `PaymentMethod`                            | | Payment Method Snapshot                       | `PaymentMethodSnapshot`                    | | Payment Method Transaction Log                | `PaymentMethodTransactionLog`              | | Payment Method Update                         | `UpdaterDetail`                            | | Payment Part                                  | `PaymentPart`                              | | Payment Part Item                             | `PaymentPartItem`                          | | Payment Run                                   | `PaymentRun`                               | | Payment Transaction Log                       | `PaymentTransactionLog`                    | | Processed Usage                               | `ProcessedUsage`                           | | Product                                       | `Product`                                  | | Product Feature                               | `ProductFeature`                           | | Product Rate Plan                             | `ProductRatePlan`                          | | Product Rate Plan Charge                      | `ProductRatePlanCharge`                    | | Product Rate Plan Charge Tier                 | `ProductRatePlanChargeTier`                | | Rate Plan                                     | `RatePlan`                                 | | Rate Plan Charge                              | `RatePlanCharge`                           | | Rate Plan Charge Tier                         | `RatePlanChargeTier`                       | | Refund                                        | `Refund`                                   | | Refund Application                            | `RefundApplication`                        | | Refund Application Item                       | `RefundApplicationItem`                    | | Refund Invoice Payment                        | `RefundInvoicePayment`                     | | Refund Part                                   | `RefundPart`                               | | Refund Part Item                              | `RefundPartItem`                           | | Refund Transaction Log                        | `RefundTransactionLog`                     | | Revenue Charge Summary                        | `RevenueChargeSummary`                     | | Revenue Charge Summary Item                   | `RevenueChargeSummaryItem`                 | | Revenue Event                                 | `RevenueEvent`                             | | Revenue Event Credit Memo Item                | `RevenueEventCreditMemoItem`               | | Revenue Event Debit Memo Item                 | `RevenueEventDebitMemoItem`                | | Revenue Event Invoice Item                    | `RevenueEventInvoiceItem`                  | | Revenue Event Invoice Item Adjustment         | `RevenueEventInvoiceItemAdjustment`        | | Revenue Event Item                            | `RevenueEventItem`                         | | Revenue Event Item Credit Memo Item           | `RevenueEventItemCreditMemoItem`           | | Revenue Event Item Debit Memo Item            | `RevenueEventItemDebitMemoItem`            | | Revenue Event Item Invoice Item               | `RevenueEventItemInvoiceItem`              | | Revenue Event Item Invoice Item Adjustment    | `RevenueEventItemInvoiceItemAdjustment`    | | Revenue Event Type                            | `RevenueEventType`                         | | Revenue Schedule                              | `RevenueSchedule`                          | | Revenue Schedule Credit Memo Item             | `RevenueScheduleCreditMemoItem`            | | Revenue Schedule Debit Memo Item              | `RevenueScheduleDebitMemoItem`             | | Revenue Schedule Invoice Item                 | `RevenueScheduleInvoiceItem`               | | Revenue Schedule Invoice Item Adjustment      | `RevenueScheduleInvoiceItemAdjustment`     | | Revenue Schedule Item                         | `RevenueScheduleItem`                      | | Revenue Schedule Item Credit Memo Item        | `RevenueScheduleItemCreditMemoItem`        | | Revenue Schedule Item Debit Memo Item         | `RevenueScheduleItemDebitMemoItem`         | | Revenue Schedule Item Invoice Item            | `RevenueScheduleItemInvoiceItem`           | | Revenue Schedule Item Invoice Item Adjustment | `RevenueScheduleItemInvoiceItemAdjustment` | | Subscription                                  | `Subscription`                             | | Subscription Product Feature                  | `SubscriptionProductFeature`               | | Taxable Item Snapshot                         | `TaxableItemSnapshot`                      | | Taxation Item                                 | `TaxationItem`                             | | Updater Batch                                 | `UpdaterBatch`                             | | Usage                                         | `Usage`                                    | 
 *
 * The version of the OpenAPI document: 2022-08-12
 * Contact: docs@zuora.com
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */


using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using OpenAPIDateConverter = ZuoraClient.Client.OpenAPIDateConverter;

namespace ZuoraClient.Model
{
    /// <summary>
    /// GETAmendmentType
    /// </summary>
    [DataContract(Name = "GETAmendmentType")]
    public partial class GETAmendmentType : IEquatable<GETAmendmentType>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GETAmendmentType" /> class.
        /// </summary>
        /// <param name="autoRenew">Determines whether the subscription is automatically renewed, or whether it expires at the end of the term and needs to be manually renewed.  .</param>
        /// <param name="baseRatePlanId">The rate plan ID on which changes are made. Only the Update or Remove amendment returns a base rate plan ID. .</param>
        /// <param name="baseSubscriptionId">The ID of the subscription based on which the amendment is created. .</param>
        /// <param name="bookingDate">The date when the amendment is signed. This field is to standardize the booking date information to increase audit ability and traceability of data between Zuora Billing and Zuora Revenue. It is mapped as the booking date for a sale order line in Zuora Revenue. .</param>
        /// <param name="code">The amendment code. .</param>
        /// <param name="contractEffectiveDate">The date when the amendment becomes effective for billing purposes, as &#x60;yyyy-mm-dd&#x60;. .</param>
        /// <param name="currentTerm">The length of the period for the current subscription term.  .</param>
        /// <param name="currentTermPeriodType">The period type for the current subscription term. Possible values are:  - Month - Year - Day - Week .</param>
        /// <param name="customerAcceptanceDate">The date when the customer accepts the amendment changes to the subscription, as &#x60;yyyy-mm-dd&#x60;. .</param>
        /// <param name="description">Description of the amendment. .</param>
        /// <param name="destinationAccountId">The ID of the account that the subscription is being transferred to. .</param>
        /// <param name="destinationInvoiceOwnerId">The ID of the invoice that the subscription is being transferred to. .</param>
        /// <param name="effectiveDate">The date when the amendment changes take effective.  .</param>
        /// <param name="effectivePolicy"> Effective Policy for the subscription. Possible values are:  - EffectiveImmediately  - EffectiveEndOfBillingPeriod  - SpecificDate  The value depends on the following conditions: * If the rate plan change (from old to new) is an upgrade, the effective policy is &#x60;EffectiveImmediately&#x60; by default. * If the rate plan change (from old to new) is a downgrade, the effective policy is &#x60;EffectiveEndOfBillingPeriod&#x60; by default. * Otherwise, the effective policy is &#x60;SpecificDate&#x60; by default.  **Note:** This feature is in the **Early Adopter** phase. We are actively soliciting feedback from a small set of early adopters before releasing it as generally available. If you want to join this early adopter program, submit a request at [Zuora Global Support](http://support.zuora.com/). .</param>
        /// <param name="id">The amendment ID. .</param>
        /// <param name="name">The name of the amendment. .</param>
        /// <param name="newRatePlanId">The ID of the rate plan charge on which amendment is made. Only the Add or Update amendment returns a new rate plan ID. .</param>
        /// <param name="newSubscriptionId">The ID of the subscription that the amendment changes. .</param>
        /// <param name="renewalSetting">Specifies whether a termed subscription will remain termed or change to evergreen when it is renewed. Possible values are:  - RENEW_WITH_SPECIFIC_TERM - RENEW_TO_EVERGREEN .</param>
        /// <param name="renewalTerm">The term of renewal for the amended subscription. .</param>
        /// <param name="renewalTermPeriodType">The period type for the subscription renewal term. Possible values are:  - Month - Year - Day - Week .</param>
        /// <param name="resumeDate">The date when the subscription resumption takes effect, as &#x60;yyyy-mm-dd&#x60;.  **Note:** This feature is in **Limited Availability**. If you wish to have access to the feature, submit a request at [Zuora Global Support](http://support.zuora.com/). .</param>
        /// <param name="serviceActivationDate">The date when service is activated, as &#x60;yyyy-mm-dd&#x60;. .</param>
        /// <param name="specificUpdateDate">The date when the Update Product amendment takes effect.  Only for the Update Product amendments if there is already a future-dated Update Product amendment on the subscription. .</param>
        /// <param name="status">The status of the amendment. Possible values are:  - Draft  - Pending Activation - Pending Acceptance - Completed .</param>
        /// <param name="subType">Sub type for the subscription.   Possible values are:  - Upgrade  - Downgrade  - Crossgrade  - PlanChanged  This field shows the sub type you choose for your change plan amendment.  However, if you do not set this field, the field will be automatically generated by the system according to the following rules:  When the old and new rate plans are within the same Grading catalog group: * If the grade of new plan is greater than that of the old plan, this is an \&quot;Upgrade\&quot;. * If the grade of new plan is less than that of the old plan, this is a \&quot;Downgrade\&quot;. * If the grade of new plan equals that of the old plan, this is a \&quot;Crossgrade\&quot;.  When the old and new rate plans are not in the same Grading catalog group, or either has no group, this is \&quot;PlanChanged\&quot;.  **Note:** This feature is in the **Early Adopter** phase. We are actively soliciting feedback from a small set of early adopters before releasing it as generally available. If you want to join this early adopter program, submit a request at [Zuora Global Support](http://support.zuora.com/). .</param>
        /// <param name="success">Returns &#x60;true&#x60; if the request was processed successfully. .</param>
        /// <param name="suspendDate">The date when the subscription suspension takes effect, as &#x60;yyyy-mm-dd&#x60;.  **Note:** This feature is in **Limited Availability**. If you wish to have access to the feature, submit a request at [Zuora Global Support](http://support.zuora.com/). .</param>
        /// <param name="termStartDate">The date when the new terms and conditions take effect. .</param>
        /// <param name="termType">Indicates if the subscription is &#x60;TERMED&#x60; or &#x60;EVERGREEN&#x60;. .</param>
        /// <param name="type">Type of the amendment. Possible values are:  - Cancellation - NewProduct - OwnerTransfer - RemoveProduct - Renewal - UpdateProduct - TermsAndConditions - ChangePlan - Note that this feature is in the **Early Adopter** phase. We are actively soliciting feedback from a small set of early adopters before releasing it as generally available. If you want to join this early adopter program, submit a request at [Zuora Global Support](http://support.zuora.com/). .</param>
        public GETAmendmentType(bool autoRenew = default(bool), string baseRatePlanId = default(string), string baseSubscriptionId = default(string), DateTime bookingDate = default(DateTime), string code = default(string), DateTime contractEffectiveDate = default(DateTime), long currentTerm = default(long), string currentTermPeriodType = default(string), DateTime customerAcceptanceDate = default(DateTime), string description = default(string), string destinationAccountId = default(string), string destinationInvoiceOwnerId = default(string), DateTime effectiveDate = default(DateTime), string effectivePolicy = default(string), string id = default(string), string name = default(string), string newRatePlanId = default(string), string newSubscriptionId = default(string), string renewalSetting = default(string), long renewalTerm = default(long), string renewalTermPeriodType = default(string), DateTime resumeDate = default(DateTime), DateTime serviceActivationDate = default(DateTime), DateTime specificUpdateDate = default(DateTime), string status = default(string), string subType = default(string), bool success = default(bool), DateTime suspendDate = default(DateTime), DateTime termStartDate = default(DateTime), string termType = default(string), string type = default(string))
        {
            this.AutoRenew = autoRenew;
            this.BaseRatePlanId = baseRatePlanId;
            this.BaseSubscriptionId = baseSubscriptionId;
            this.BookingDate = bookingDate;
            this.Code = code;
            this.ContractEffectiveDate = contractEffectiveDate;
            this.CurrentTerm = currentTerm;
            this.CurrentTermPeriodType = currentTermPeriodType;
            this.CustomerAcceptanceDate = customerAcceptanceDate;
            this.Description = description;
            this.DestinationAccountId = destinationAccountId;
            this.DestinationInvoiceOwnerId = destinationInvoiceOwnerId;
            this.EffectiveDate = effectiveDate;
            this.EffectivePolicy = effectivePolicy;
            this.Id = id;
            this.Name = name;
            this.NewRatePlanId = newRatePlanId;
            this.NewSubscriptionId = newSubscriptionId;
            this.RenewalSetting = renewalSetting;
            this.RenewalTerm = renewalTerm;
            this.RenewalTermPeriodType = renewalTermPeriodType;
            this.ResumeDate = resumeDate;
            this.ServiceActivationDate = serviceActivationDate;
            this.SpecificUpdateDate = specificUpdateDate;
            this.Status = status;
            this.SubType = subType;
            this.Success = success;
            this.SuspendDate = suspendDate;
            this.TermStartDate = termStartDate;
            this.TermType = termType;
            this.Type = type;
        }

        /// <summary>
        /// Determines whether the subscription is automatically renewed, or whether it expires at the end of the term and needs to be manually renewed.  
        /// </summary>
        /// <value>Determines whether the subscription is automatically renewed, or whether it expires at the end of the term and needs to be manually renewed.  </value>
        [DataMember(Name = "autoRenew", EmitDefaultValue = true)]
        public bool AutoRenew { get; set; }

        /// <summary>
        /// The rate plan ID on which changes are made. Only the Update or Remove amendment returns a base rate plan ID. 
        /// </summary>
        /// <value>The rate plan ID on which changes are made. Only the Update or Remove amendment returns a base rate plan ID. </value>
        [DataMember(Name = "baseRatePlanId", EmitDefaultValue = false)]
        public string BaseRatePlanId { get; set; }

        /// <summary>
        /// The ID of the subscription based on which the amendment is created. 
        /// </summary>
        /// <value>The ID of the subscription based on which the amendment is created. </value>
        [DataMember(Name = "baseSubscriptionId", EmitDefaultValue = false)]
        public string BaseSubscriptionId { get; set; }

        /// <summary>
        /// The date when the amendment is signed. This field is to standardize the booking date information to increase audit ability and traceability of data between Zuora Billing and Zuora Revenue. It is mapped as the booking date for a sale order line in Zuora Revenue. 
        /// </summary>
        /// <value>The date when the amendment is signed. This field is to standardize the booking date information to increase audit ability and traceability of data between Zuora Billing and Zuora Revenue. It is mapped as the booking date for a sale order line in Zuora Revenue. </value>
        [DataMember(Name = "bookingDate", EmitDefaultValue = false)]
        [JsonConverter(typeof(OpenAPIDateConverter))]
        public DateTime BookingDate { get; set; }

        /// <summary>
        /// The amendment code. 
        /// </summary>
        /// <value>The amendment code. </value>
        [DataMember(Name = "code", EmitDefaultValue = false)]
        public string Code { get; set; }

        /// <summary>
        /// The date when the amendment becomes effective for billing purposes, as &#x60;yyyy-mm-dd&#x60;. 
        /// </summary>
        /// <value>The date when the amendment becomes effective for billing purposes, as &#x60;yyyy-mm-dd&#x60;. </value>
        [DataMember(Name = "contractEffectiveDate", EmitDefaultValue = false)]
        [JsonConverter(typeof(OpenAPIDateConverter))]
        public DateTime ContractEffectiveDate { get; set; }

        /// <summary>
        /// The length of the period for the current subscription term.  
        /// </summary>
        /// <value>The length of the period for the current subscription term.  </value>
        [DataMember(Name = "currentTerm", EmitDefaultValue = false)]
        public long CurrentTerm { get; set; }

        /// <summary>
        /// The period type for the current subscription term. Possible values are:  - Month - Year - Day - Week 
        /// </summary>
        /// <value>The period type for the current subscription term. Possible values are:  - Month - Year - Day - Week </value>
        [DataMember(Name = "currentTermPeriodType", EmitDefaultValue = false)]
        public string CurrentTermPeriodType { get; set; }

        /// <summary>
        /// The date when the customer accepts the amendment changes to the subscription, as &#x60;yyyy-mm-dd&#x60;. 
        /// </summary>
        /// <value>The date when the customer accepts the amendment changes to the subscription, as &#x60;yyyy-mm-dd&#x60;. </value>
        [DataMember(Name = "customerAcceptanceDate", EmitDefaultValue = false)]
        [JsonConverter(typeof(OpenAPIDateConverter))]
        public DateTime CustomerAcceptanceDate { get; set; }

        /// <summary>
        /// Description of the amendment. 
        /// </summary>
        /// <value>Description of the amendment. </value>
        [DataMember(Name = "description", EmitDefaultValue = false)]
        public string Description { get; set; }

        /// <summary>
        /// The ID of the account that the subscription is being transferred to. 
        /// </summary>
        /// <value>The ID of the account that the subscription is being transferred to. </value>
        [DataMember(Name = "destinationAccountId", EmitDefaultValue = false)]
        public string DestinationAccountId { get; set; }

        /// <summary>
        /// The ID of the invoice that the subscription is being transferred to. 
        /// </summary>
        /// <value>The ID of the invoice that the subscription is being transferred to. </value>
        [DataMember(Name = "destinationInvoiceOwnerId", EmitDefaultValue = false)]
        public string DestinationInvoiceOwnerId { get; set; }

        /// <summary>
        /// The date when the amendment changes take effective.  
        /// </summary>
        /// <value>The date when the amendment changes take effective.  </value>
        [DataMember(Name = "effectiveDate", EmitDefaultValue = false)]
        [JsonConverter(typeof(OpenAPIDateConverter))]
        public DateTime EffectiveDate { get; set; }

        /// <summary>
        ///  Effective Policy for the subscription. Possible values are:  - EffectiveImmediately  - EffectiveEndOfBillingPeriod  - SpecificDate  The value depends on the following conditions: * If the rate plan change (from old to new) is an upgrade, the effective policy is &#x60;EffectiveImmediately&#x60; by default. * If the rate plan change (from old to new) is a downgrade, the effective policy is &#x60;EffectiveEndOfBillingPeriod&#x60; by default. * Otherwise, the effective policy is &#x60;SpecificDate&#x60; by default.  **Note:** This feature is in the **Early Adopter** phase. We are actively soliciting feedback from a small set of early adopters before releasing it as generally available. If you want to join this early adopter program, submit a request at [Zuora Global Support](http://support.zuora.com/). 
        /// </summary>
        /// <value> Effective Policy for the subscription. Possible values are:  - EffectiveImmediately  - EffectiveEndOfBillingPeriod  - SpecificDate  The value depends on the following conditions: * If the rate plan change (from old to new) is an upgrade, the effective policy is &#x60;EffectiveImmediately&#x60; by default. * If the rate plan change (from old to new) is a downgrade, the effective policy is &#x60;EffectiveEndOfBillingPeriod&#x60; by default. * Otherwise, the effective policy is &#x60;SpecificDate&#x60; by default.  **Note:** This feature is in the **Early Adopter** phase. We are actively soliciting feedback from a small set of early adopters before releasing it as generally available. If you want to join this early adopter program, submit a request at [Zuora Global Support](http://support.zuora.com/). </value>
        [DataMember(Name = "effectivePolicy", EmitDefaultValue = false)]
        public string EffectivePolicy { get; set; }

        /// <summary>
        /// The amendment ID. 
        /// </summary>
        /// <value>The amendment ID. </value>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }

        /// <summary>
        /// The name of the amendment. 
        /// </summary>
        /// <value>The name of the amendment. </value>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        /// The ID of the rate plan charge on which amendment is made. Only the Add or Update amendment returns a new rate plan ID. 
        /// </summary>
        /// <value>The ID of the rate plan charge on which amendment is made. Only the Add or Update amendment returns a new rate plan ID. </value>
        [DataMember(Name = "newRatePlanId", EmitDefaultValue = false)]
        public string NewRatePlanId { get; set; }

        /// <summary>
        /// The ID of the subscription that the amendment changes. 
        /// </summary>
        /// <value>The ID of the subscription that the amendment changes. </value>
        [DataMember(Name = "newSubscriptionId", EmitDefaultValue = false)]
        public string NewSubscriptionId { get; set; }

        /// <summary>
        /// Specifies whether a termed subscription will remain termed or change to evergreen when it is renewed. Possible values are:  - RENEW_WITH_SPECIFIC_TERM - RENEW_TO_EVERGREEN 
        /// </summary>
        /// <value>Specifies whether a termed subscription will remain termed or change to evergreen when it is renewed. Possible values are:  - RENEW_WITH_SPECIFIC_TERM - RENEW_TO_EVERGREEN </value>
        [DataMember(Name = "renewalSetting", EmitDefaultValue = false)]
        public string RenewalSetting { get; set; }

        /// <summary>
        /// The term of renewal for the amended subscription. 
        /// </summary>
        /// <value>The term of renewal for the amended subscription. </value>
        [DataMember(Name = "renewalTerm", EmitDefaultValue = false)]
        public long RenewalTerm { get; set; }

        /// <summary>
        /// The period type for the subscription renewal term. Possible values are:  - Month - Year - Day - Week 
        /// </summary>
        /// <value>The period type for the subscription renewal term. Possible values are:  - Month - Year - Day - Week </value>
        [DataMember(Name = "renewalTermPeriodType", EmitDefaultValue = false)]
        public string RenewalTermPeriodType { get; set; }

        /// <summary>
        /// The date when the subscription resumption takes effect, as &#x60;yyyy-mm-dd&#x60;.  **Note:** This feature is in **Limited Availability**. If you wish to have access to the feature, submit a request at [Zuora Global Support](http://support.zuora.com/). 
        /// </summary>
        /// <value>The date when the subscription resumption takes effect, as &#x60;yyyy-mm-dd&#x60;.  **Note:** This feature is in **Limited Availability**. If you wish to have access to the feature, submit a request at [Zuora Global Support](http://support.zuora.com/). </value>
        [DataMember(Name = "resumeDate", EmitDefaultValue = false)]
        [JsonConverter(typeof(OpenAPIDateConverter))]
        public DateTime ResumeDate { get; set; }

        /// <summary>
        /// The date when service is activated, as &#x60;yyyy-mm-dd&#x60;. 
        /// </summary>
        /// <value>The date when service is activated, as &#x60;yyyy-mm-dd&#x60;. </value>
        [DataMember(Name = "serviceActivationDate", EmitDefaultValue = false)]
        [JsonConverter(typeof(OpenAPIDateConverter))]
        public DateTime ServiceActivationDate { get; set; }

        /// <summary>
        /// The date when the Update Product amendment takes effect.  Only for the Update Product amendments if there is already a future-dated Update Product amendment on the subscription. 
        /// </summary>
        /// <value>The date when the Update Product amendment takes effect.  Only for the Update Product amendments if there is already a future-dated Update Product amendment on the subscription. </value>
        [DataMember(Name = "specificUpdateDate", EmitDefaultValue = false)]
        [JsonConverter(typeof(OpenAPIDateConverter))]
        public DateTime SpecificUpdateDate { get; set; }

        /// <summary>
        /// The status of the amendment. Possible values are:  - Draft  - Pending Activation - Pending Acceptance - Completed 
        /// </summary>
        /// <value>The status of the amendment. Possible values are:  - Draft  - Pending Activation - Pending Acceptance - Completed </value>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public string Status { get; set; }

        /// <summary>
        /// Sub type for the subscription.   Possible values are:  - Upgrade  - Downgrade  - Crossgrade  - PlanChanged  This field shows the sub type you choose for your change plan amendment.  However, if you do not set this field, the field will be automatically generated by the system according to the following rules:  When the old and new rate plans are within the same Grading catalog group: * If the grade of new plan is greater than that of the old plan, this is an \&quot;Upgrade\&quot;. * If the grade of new plan is less than that of the old plan, this is a \&quot;Downgrade\&quot;. * If the grade of new plan equals that of the old plan, this is a \&quot;Crossgrade\&quot;.  When the old and new rate plans are not in the same Grading catalog group, or either has no group, this is \&quot;PlanChanged\&quot;.  **Note:** This feature is in the **Early Adopter** phase. We are actively soliciting feedback from a small set of early adopters before releasing it as generally available. If you want to join this early adopter program, submit a request at [Zuora Global Support](http://support.zuora.com/). 
        /// </summary>
        /// <value>Sub type for the subscription.   Possible values are:  - Upgrade  - Downgrade  - Crossgrade  - PlanChanged  This field shows the sub type you choose for your change plan amendment.  However, if you do not set this field, the field will be automatically generated by the system according to the following rules:  When the old and new rate plans are within the same Grading catalog group: * If the grade of new plan is greater than that of the old plan, this is an \&quot;Upgrade\&quot;. * If the grade of new plan is less than that of the old plan, this is a \&quot;Downgrade\&quot;. * If the grade of new plan equals that of the old plan, this is a \&quot;Crossgrade\&quot;.  When the old and new rate plans are not in the same Grading catalog group, or either has no group, this is \&quot;PlanChanged\&quot;.  **Note:** This feature is in the **Early Adopter** phase. We are actively soliciting feedback from a small set of early adopters before releasing it as generally available. If you want to join this early adopter program, submit a request at [Zuora Global Support](http://support.zuora.com/). </value>
        [DataMember(Name = "subType", EmitDefaultValue = false)]
        public string SubType { get; set; }

        /// <summary>
        /// Returns &#x60;true&#x60; if the request was processed successfully. 
        /// </summary>
        /// <value>Returns &#x60;true&#x60; if the request was processed successfully. </value>
        [DataMember(Name = "success", EmitDefaultValue = true)]
        public bool Success { get; set; }

        /// <summary>
        /// The date when the subscription suspension takes effect, as &#x60;yyyy-mm-dd&#x60;.  **Note:** This feature is in **Limited Availability**. If you wish to have access to the feature, submit a request at [Zuora Global Support](http://support.zuora.com/). 
        /// </summary>
        /// <value>The date when the subscription suspension takes effect, as &#x60;yyyy-mm-dd&#x60;.  **Note:** This feature is in **Limited Availability**. If you wish to have access to the feature, submit a request at [Zuora Global Support](http://support.zuora.com/). </value>
        [DataMember(Name = "suspendDate", EmitDefaultValue = false)]
        [JsonConverter(typeof(OpenAPIDateConverter))]
        public DateTime SuspendDate { get; set; }

        /// <summary>
        /// The date when the new terms and conditions take effect. 
        /// </summary>
        /// <value>The date when the new terms and conditions take effect. </value>
        [DataMember(Name = "termStartDate", EmitDefaultValue = false)]
        [JsonConverter(typeof(OpenAPIDateConverter))]
        public DateTime TermStartDate { get; set; }

        /// <summary>
        /// Indicates if the subscription is &#x60;TERMED&#x60; or &#x60;EVERGREEN&#x60;. 
        /// </summary>
        /// <value>Indicates if the subscription is &#x60;TERMED&#x60; or &#x60;EVERGREEN&#x60;. </value>
        [DataMember(Name = "termType", EmitDefaultValue = false)]
        public string TermType { get; set; }

        /// <summary>
        /// Type of the amendment. Possible values are:  - Cancellation - NewProduct - OwnerTransfer - RemoveProduct - Renewal - UpdateProduct - TermsAndConditions - ChangePlan - Note that this feature is in the **Early Adopter** phase. We are actively soliciting feedback from a small set of early adopters before releasing it as generally available. If you want to join this early adopter program, submit a request at [Zuora Global Support](http://support.zuora.com/). 
        /// </summary>
        /// <value>Type of the amendment. Possible values are:  - Cancellation - NewProduct - OwnerTransfer - RemoveProduct - Renewal - UpdateProduct - TermsAndConditions - ChangePlan - Note that this feature is in the **Early Adopter** phase. We are actively soliciting feedback from a small set of early adopters before releasing it as generally available. If you want to join this early adopter program, submit a request at [Zuora Global Support](http://support.zuora.com/). </value>
        [DataMember(Name = "type", EmitDefaultValue = false)]
        public string Type { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class GETAmendmentType {\n");
            sb.Append("  AutoRenew: ").Append(AutoRenew).Append("\n");
            sb.Append("  BaseRatePlanId: ").Append(BaseRatePlanId).Append("\n");
            sb.Append("  BaseSubscriptionId: ").Append(BaseSubscriptionId).Append("\n");
            sb.Append("  BookingDate: ").Append(BookingDate).Append("\n");
            sb.Append("  Code: ").Append(Code).Append("\n");
            sb.Append("  ContractEffectiveDate: ").Append(ContractEffectiveDate).Append("\n");
            sb.Append("  CurrentTerm: ").Append(CurrentTerm).Append("\n");
            sb.Append("  CurrentTermPeriodType: ").Append(CurrentTermPeriodType).Append("\n");
            sb.Append("  CustomerAcceptanceDate: ").Append(CustomerAcceptanceDate).Append("\n");
            sb.Append("  Description: ").Append(Description).Append("\n");
            sb.Append("  DestinationAccountId: ").Append(DestinationAccountId).Append("\n");
            sb.Append("  DestinationInvoiceOwnerId: ").Append(DestinationInvoiceOwnerId).Append("\n");
            sb.Append("  EffectiveDate: ").Append(EffectiveDate).Append("\n");
            sb.Append("  EffectivePolicy: ").Append(EffectivePolicy).Append("\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  NewRatePlanId: ").Append(NewRatePlanId).Append("\n");
            sb.Append("  NewSubscriptionId: ").Append(NewSubscriptionId).Append("\n");
            sb.Append("  RenewalSetting: ").Append(RenewalSetting).Append("\n");
            sb.Append("  RenewalTerm: ").Append(RenewalTerm).Append("\n");
            sb.Append("  RenewalTermPeriodType: ").Append(RenewalTermPeriodType).Append("\n");
            sb.Append("  ResumeDate: ").Append(ResumeDate).Append("\n");
            sb.Append("  ServiceActivationDate: ").Append(ServiceActivationDate).Append("\n");
            sb.Append("  SpecificUpdateDate: ").Append(SpecificUpdateDate).Append("\n");
            sb.Append("  Status: ").Append(Status).Append("\n");
            sb.Append("  SubType: ").Append(SubType).Append("\n");
            sb.Append("  Success: ").Append(Success).Append("\n");
            sb.Append("  SuspendDate: ").Append(SuspendDate).Append("\n");
            sb.Append("  TermStartDate: ").Append(TermStartDate).Append("\n");
            sb.Append("  TermType: ").Append(TermType).Append("\n");
            sb.Append("  Type: ").Append(Type).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return this.Equals(input as GETAmendmentType);
        }

        /// <summary>
        /// Returns true if GETAmendmentType instances are equal
        /// </summary>
        /// <param name="input">Instance of GETAmendmentType to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(GETAmendmentType input)
        {
            if (input == null)
            {
                return false;
            }
            return 
                (
                    this.AutoRenew == input.AutoRenew ||
                    this.AutoRenew.Equals(input.AutoRenew)
                ) && 
                (
                    this.BaseRatePlanId == input.BaseRatePlanId ||
                    (this.BaseRatePlanId != null &&
                    this.BaseRatePlanId.Equals(input.BaseRatePlanId))
                ) && 
                (
                    this.BaseSubscriptionId == input.BaseSubscriptionId ||
                    (this.BaseSubscriptionId != null &&
                    this.BaseSubscriptionId.Equals(input.BaseSubscriptionId))
                ) && 
                (
                    this.BookingDate == input.BookingDate ||
                    (this.BookingDate != null &&
                    this.BookingDate.Equals(input.BookingDate))
                ) && 
                (
                    this.Code == input.Code ||
                    (this.Code != null &&
                    this.Code.Equals(input.Code))
                ) && 
                (
                    this.ContractEffectiveDate == input.ContractEffectiveDate ||
                    (this.ContractEffectiveDate != null &&
                    this.ContractEffectiveDate.Equals(input.ContractEffectiveDate))
                ) && 
                (
                    this.CurrentTerm == input.CurrentTerm ||
                    this.CurrentTerm.Equals(input.CurrentTerm)
                ) && 
                (
                    this.CurrentTermPeriodType == input.CurrentTermPeriodType ||
                    (this.CurrentTermPeriodType != null &&
                    this.CurrentTermPeriodType.Equals(input.CurrentTermPeriodType))
                ) && 
                (
                    this.CustomerAcceptanceDate == input.CustomerAcceptanceDate ||
                    (this.CustomerAcceptanceDate != null &&
                    this.CustomerAcceptanceDate.Equals(input.CustomerAcceptanceDate))
                ) && 
                (
                    this.Description == input.Description ||
                    (this.Description != null &&
                    this.Description.Equals(input.Description))
                ) && 
                (
                    this.DestinationAccountId == input.DestinationAccountId ||
                    (this.DestinationAccountId != null &&
                    this.DestinationAccountId.Equals(input.DestinationAccountId))
                ) && 
                (
                    this.DestinationInvoiceOwnerId == input.DestinationInvoiceOwnerId ||
                    (this.DestinationInvoiceOwnerId != null &&
                    this.DestinationInvoiceOwnerId.Equals(input.DestinationInvoiceOwnerId))
                ) && 
                (
                    this.EffectiveDate == input.EffectiveDate ||
                    (this.EffectiveDate != null &&
                    this.EffectiveDate.Equals(input.EffectiveDate))
                ) && 
                (
                    this.EffectivePolicy == input.EffectivePolicy ||
                    (this.EffectivePolicy != null &&
                    this.EffectivePolicy.Equals(input.EffectivePolicy))
                ) && 
                (
                    this.Id == input.Id ||
                    (this.Id != null &&
                    this.Id.Equals(input.Id))
                ) && 
                (
                    this.Name == input.Name ||
                    (this.Name != null &&
                    this.Name.Equals(input.Name))
                ) && 
                (
                    this.NewRatePlanId == input.NewRatePlanId ||
                    (this.NewRatePlanId != null &&
                    this.NewRatePlanId.Equals(input.NewRatePlanId))
                ) && 
                (
                    this.NewSubscriptionId == input.NewSubscriptionId ||
                    (this.NewSubscriptionId != null &&
                    this.NewSubscriptionId.Equals(input.NewSubscriptionId))
                ) && 
                (
                    this.RenewalSetting == input.RenewalSetting ||
                    (this.RenewalSetting != null &&
                    this.RenewalSetting.Equals(input.RenewalSetting))
                ) && 
                (
                    this.RenewalTerm == input.RenewalTerm ||
                    this.RenewalTerm.Equals(input.RenewalTerm)
                ) && 
                (
                    this.RenewalTermPeriodType == input.RenewalTermPeriodType ||
                    (this.RenewalTermPeriodType != null &&
                    this.RenewalTermPeriodType.Equals(input.RenewalTermPeriodType))
                ) && 
                (
                    this.ResumeDate == input.ResumeDate ||
                    (this.ResumeDate != null &&
                    this.ResumeDate.Equals(input.ResumeDate))
                ) && 
                (
                    this.ServiceActivationDate == input.ServiceActivationDate ||
                    (this.ServiceActivationDate != null &&
                    this.ServiceActivationDate.Equals(input.ServiceActivationDate))
                ) && 
                (
                    this.SpecificUpdateDate == input.SpecificUpdateDate ||
                    (this.SpecificUpdateDate != null &&
                    this.SpecificUpdateDate.Equals(input.SpecificUpdateDate))
                ) && 
                (
                    this.Status == input.Status ||
                    (this.Status != null &&
                    this.Status.Equals(input.Status))
                ) && 
                (
                    this.SubType == input.SubType ||
                    (this.SubType != null &&
                    this.SubType.Equals(input.SubType))
                ) && 
                (
                    this.Success == input.Success ||
                    this.Success.Equals(input.Success)
                ) && 
                (
                    this.SuspendDate == input.SuspendDate ||
                    (this.SuspendDate != null &&
                    this.SuspendDate.Equals(input.SuspendDate))
                ) && 
                (
                    this.TermStartDate == input.TermStartDate ||
                    (this.TermStartDate != null &&
                    this.TermStartDate.Equals(input.TermStartDate))
                ) && 
                (
                    this.TermType == input.TermType ||
                    (this.TermType != null &&
                    this.TermType.Equals(input.TermType))
                ) && 
                (
                    this.Type == input.Type ||
                    (this.Type != null &&
                    this.Type.Equals(input.Type))
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hashCode = 41;
                hashCode = (hashCode * 59) + this.AutoRenew.GetHashCode();
                if (this.BaseRatePlanId != null)
                {
                    hashCode = (hashCode * 59) + this.BaseRatePlanId.GetHashCode();
                }
                if (this.BaseSubscriptionId != null)
                {
                    hashCode = (hashCode * 59) + this.BaseSubscriptionId.GetHashCode();
                }
                if (this.BookingDate != null)
                {
                    hashCode = (hashCode * 59) + this.BookingDate.GetHashCode();
                }
                if (this.Code != null)
                {
                    hashCode = (hashCode * 59) + this.Code.GetHashCode();
                }
                if (this.ContractEffectiveDate != null)
                {
                    hashCode = (hashCode * 59) + this.ContractEffectiveDate.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.CurrentTerm.GetHashCode();
                if (this.CurrentTermPeriodType != null)
                {
                    hashCode = (hashCode * 59) + this.CurrentTermPeriodType.GetHashCode();
                }
                if (this.CustomerAcceptanceDate != null)
                {
                    hashCode = (hashCode * 59) + this.CustomerAcceptanceDate.GetHashCode();
                }
                if (this.Description != null)
                {
                    hashCode = (hashCode * 59) + this.Description.GetHashCode();
                }
                if (this.DestinationAccountId != null)
                {
                    hashCode = (hashCode * 59) + this.DestinationAccountId.GetHashCode();
                }
                if (this.DestinationInvoiceOwnerId != null)
                {
                    hashCode = (hashCode * 59) + this.DestinationInvoiceOwnerId.GetHashCode();
                }
                if (this.EffectiveDate != null)
                {
                    hashCode = (hashCode * 59) + this.EffectiveDate.GetHashCode();
                }
                if (this.EffectivePolicy != null)
                {
                    hashCode = (hashCode * 59) + this.EffectivePolicy.GetHashCode();
                }
                if (this.Id != null)
                {
                    hashCode = (hashCode * 59) + this.Id.GetHashCode();
                }
                if (this.Name != null)
                {
                    hashCode = (hashCode * 59) + this.Name.GetHashCode();
                }
                if (this.NewRatePlanId != null)
                {
                    hashCode = (hashCode * 59) + this.NewRatePlanId.GetHashCode();
                }
                if (this.NewSubscriptionId != null)
                {
                    hashCode = (hashCode * 59) + this.NewSubscriptionId.GetHashCode();
                }
                if (this.RenewalSetting != null)
                {
                    hashCode = (hashCode * 59) + this.RenewalSetting.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.RenewalTerm.GetHashCode();
                if (this.RenewalTermPeriodType != null)
                {
                    hashCode = (hashCode * 59) + this.RenewalTermPeriodType.GetHashCode();
                }
                if (this.ResumeDate != null)
                {
                    hashCode = (hashCode * 59) + this.ResumeDate.GetHashCode();
                }
                if (this.ServiceActivationDate != null)
                {
                    hashCode = (hashCode * 59) + this.ServiceActivationDate.GetHashCode();
                }
                if (this.SpecificUpdateDate != null)
                {
                    hashCode = (hashCode * 59) + this.SpecificUpdateDate.GetHashCode();
                }
                if (this.Status != null)
                {
                    hashCode = (hashCode * 59) + this.Status.GetHashCode();
                }
                if (this.SubType != null)
                {
                    hashCode = (hashCode * 59) + this.SubType.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.Success.GetHashCode();
                if (this.SuspendDate != null)
                {
                    hashCode = (hashCode * 59) + this.SuspendDate.GetHashCode();
                }
                if (this.TermStartDate != null)
                {
                    hashCode = (hashCode * 59) + this.TermStartDate.GetHashCode();
                }
                if (this.TermType != null)
                {
                    hashCode = (hashCode * 59) + this.TermType.GetHashCode();
                }
                if (this.Type != null)
                {
                    hashCode = (hashCode * 59) + this.Type.GetHashCode();
                }
                return hashCode;
            }
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        public IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }

}
