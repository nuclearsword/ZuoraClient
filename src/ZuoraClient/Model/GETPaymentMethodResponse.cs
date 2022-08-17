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
    /// GETPaymentMethodResponse
    /// </summary>
    [DataContract(Name = "GETPaymentMethodResponse")]
    public partial class GETPaymentMethodResponse : IEquatable<GETPaymentMethodResponse>, IValidatableObject
    {
        /// <summary>
        /// Indicates whether the mandate is an existing mandate. 
        /// </summary>
        /// <value>Indicates whether the mandate is an existing mandate. </value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum ExistingMandateEnum
        {
            /// <summary>
            /// Enum Yes for value: Yes
            /// </summary>
            [EnumMember(Value = "Yes")]
            Yes = 1,

            /// <summary>
            /// Enum No for value: No
            /// </summary>
            [EnumMember(Value = "No")]
            No = 2

        }


        /// <summary>
        /// Indicates whether the mandate is an existing mandate. 
        /// </summary>
        /// <value>Indicates whether the mandate is an existing mandate. </value>
        [DataMember(Name = "existingMandate", EmitDefaultValue = false)]
        public ExistingMandateEnum? ExistingMandate { get; set; }
        /// <summary>
        /// The status of the payment method. 
        /// </summary>
        /// <value>The status of the payment method. </value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum StatusEnum
        {
            /// <summary>
            /// Enum Active for value: Active
            /// </summary>
            [EnumMember(Value = "Active")]
            Active = 1,

            /// <summary>
            /// Enum Closed for value: Closed
            /// </summary>
            [EnumMember(Value = "Closed")]
            Closed = 2,

            /// <summary>
            /// Enum Scrubbed for value: Scrubbed
            /// </summary>
            [EnumMember(Value = "Scrubbed")]
            Scrubbed = 3

        }


        /// <summary>
        /// The status of the payment method. 
        /// </summary>
        /// <value>The status of the payment method. </value>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public StatusEnum? Status { get; set; }
        /// <summary>
        /// The type of bank account associated with the ACH payment. This field is only required if the &#x60;type&#x60; field is set to &#x60;ACH&#x60;.  When creating an ACH payment method on Adyen, this field is required by Zuora but it is not required by Adyen. To create the ACH payment method successfully, specify a real value for this field if you can. If it is not possible to get the real value for it, specify any of the allowed values as a dummy value, &#x60;Checking&#x60; preferably. 
        /// </summary>
        /// <value>The type of bank account associated with the ACH payment. This field is only required if the &#x60;type&#x60; field is set to &#x60;ACH&#x60;.  When creating an ACH payment method on Adyen, this field is required by Zuora but it is not required by Adyen. To create the ACH payment method successfully, specify a real value for this field if you can. If it is not possible to get the real value for it, specify any of the allowed values as a dummy value, &#x60;Checking&#x60; preferably. </value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum BankAccountTypeEnum
        {
            /// <summary>
            /// Enum BusinessChecking for value: BusinessChecking
            /// </summary>
            [EnumMember(Value = "BusinessChecking")]
            BusinessChecking = 1,

            /// <summary>
            /// Enum BusinessSaving for value: BusinessSaving
            /// </summary>
            [EnumMember(Value = "BusinessSaving")]
            BusinessSaving = 2,

            /// <summary>
            /// Enum Checking for value: Checking
            /// </summary>
            [EnumMember(Value = "Checking")]
            Checking = 3,

            /// <summary>
            /// Enum Saving for value: Saving
            /// </summary>
            [EnumMember(Value = "Saving")]
            Saving = 4

        }


        /// <summary>
        /// The type of bank account associated with the ACH payment. This field is only required if the &#x60;type&#x60; field is set to &#x60;ACH&#x60;.  When creating an ACH payment method on Adyen, this field is required by Zuora but it is not required by Adyen. To create the ACH payment method successfully, specify a real value for this field if you can. If it is not possible to get the real value for it, specify any of the allowed values as a dummy value, &#x60;Checking&#x60; preferably. 
        /// </summary>
        /// <value>The type of bank account associated with the ACH payment. This field is only required if the &#x60;type&#x60; field is set to &#x60;ACH&#x60;.  When creating an ACH payment method on Adyen, this field is required by Zuora but it is not required by Adyen. To create the ACH payment method successfully, specify a real value for this field if you can. If it is not possible to get the real value for it, specify any of the allowed values as a dummy value, &#x60;Checking&#x60; preferably. </value>
        [DataMember(Name = "bankAccountType", EmitDefaultValue = false)]
        public BankAccountTypeEnum? BankAccountType { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="GETPaymentMethodResponse" /> class.
        /// </summary>
        /// <param name="accountHolderInfo">accountHolderInfo.</param>
        /// <param name="bankIdentificationNumber">The first six or eight digits of the payment method&#39;s number, such as the credit card number or account number. Banks use this number to identify a payment method. .</param>
        /// <param name="createdBy">ID of the user who created this payment method..</param>
        /// <param name="createdOn">The date and time when the payment method was created, in &#x60;yyyy-mm-dd hh:mm:ss&#x60; format. .</param>
        /// <param name="creditCardMaskNumber">The masked credit card number, such as: &#x60;&#x60;&#x60; *********1112 &#x60;&#x60;&#x60; **Note:** This field is only returned for Credit Card Reference Transaction payment type. .</param>
        /// <param name="creditCardType">The type of the credit card or debit card.  Possible values include &#x60;Visa&#x60;, &#x60;MasterCard&#x60;, &#x60;AmericanExpress&#x60;, &#x60;Discover&#x60;, &#x60;JCB&#x60;, and &#x60;Diners&#x60;. For more information about credit card types supported by different payment gateways, see [Supported Payment Gateways](https://knowledgecenter.zuora.com/CB_Billing/M_Payment_Gateways/Supported_Payment_Gateways).  **Note:** This field is only returned for the Credit Card and Debit Card payment types. .</param>
        /// <param name="deviceSessionId">The session ID of the user when the &#x60;PaymentMethod&#x60; was created or updated. .</param>
        /// <param name="existingMandate">Indicates whether the mandate is an existing mandate. .</param>
        /// <param name="id">The payment method ID. .</param>
        /// <param name="ipAddress">The IP address of the user when the payment method was created or updated. .</param>
        /// <param name="isDefault">Indicates whether this payment method is the default payment method for the account. .</param>
        /// <param name="lastFailedSaleTransactionDate">The date of the last failed attempt to collect payment with this payment method. .</param>
        /// <param name="lastTransaction">ID of the last transaction of this payment method..</param>
        /// <param name="lastTransactionTime">The time when the last transaction of this payment method happened..</param>
        /// <param name="mandateInfo">mandateInfo.</param>
        /// <param name="maxConsecutivePaymentFailures">The number of allowable consecutive failures Zuora attempts with the payment method before stopping. .</param>
        /// <param name="numConsecutiveFailures">The number of consecutive failed payments for this payment method. It is reset to &#x60;0&#x60; upon successful payment.  .</param>
        /// <param name="paymentRetryWindow">The retry interval setting, which prevents making a payment attempt if the last failed attempt was within the last specified number of hours. .</param>
        /// <param name="secondTokenId">A gateway unique identifier that replaces sensitive payment method data.  **Note:** This field is only returned for the Credit Card Reference Transaction payment type. .</param>
        /// <param name="status">The status of the payment method. .</param>
        /// <param name="tokenId">A gateway unique identifier that replaces sensitive payment method data or represents a gateway&#39;s unique customer profile.  **Note:** This field is only returned for the Credit Card Reference Transaction payment type. .</param>
        /// <param name="totalNumberOfErrorPayments">The number of error payments that used this payment method. .</param>
        /// <param name="totalNumberOfProcessedPayments">The number of successful payments that used this payment method. .</param>
        /// <param name="type">The type of the payment method. For example, &#x60;CreditCard&#x60;. .</param>
        /// <param name="updatedBy">ID of the user who made the last update to this payment method..</param>
        /// <param name="updatedOn">The last date and time when the payment method was updated, in &#x60;yyyy-mm-dd hh:mm:ss&#x60; format. .</param>
        /// <param name="useDefaultRetryRule">Indicates whether this payment method uses the default retry rules configured in the Zuora Payments settings. .</param>
        /// <param name="iBAN">The International Bank Account Number used to create the SEPA payment method. The value is masked. .</param>
        /// <param name="accountNumber">The number of the customer&#39;s bank account and it is masked. .</param>
        /// <param name="bankCode">The sort code or number that identifies the bank. This is also known as the sort code.          .</param>
        /// <param name="bankTransferType">The type of the Bank Transfer payment method. For example, &#x60;SEPA&#x60;. .</param>
        /// <param name="branchCode">The branch code of the bank used for Direct Debit.           .</param>
        /// <param name="businessIdentificationCode">The BIC code used for SEPA. The value is masked.        .</param>
        /// <param name="identityNumber">The identity number used for Bank Transfer.         .</param>
        /// <param name="bankABACode">The nine-digit routing number or ABA number used by banks. This field is only required if the &#x60;type&#x60; field is set to &#x60;ACH&#x60;. .</param>
        /// <param name="bankAccountName">The name of the account holder, which can be either a person or a company. This field is only required if the &#x60;type&#x60; field is set to &#x60;ACH&#x60;. .</param>
        /// <param name="bankAccountNumber">The bank account number associated with the ACH payment. This field is only required if the &#x60;type&#x60; field is set to &#x60;ACH&#x60;. .</param>
        /// <param name="bankAccountType">The type of bank account associated with the ACH payment. This field is only required if the &#x60;type&#x60; field is set to &#x60;ACH&#x60;.  When creating an ACH payment method on Adyen, this field is required by Zuora but it is not required by Adyen. To create the ACH payment method successfully, specify a real value for this field if you can. If it is not possible to get the real value for it, specify any of the allowed values as a dummy value, &#x60;Checking&#x60; preferably. .</param>
        /// <param name="bankName">The name of the bank where the ACH payment account is held. This field is only required if the &#x60;type&#x60; field is set to &#x60;ACH&#x60;.  When creating an ACH payment method on Adyen, this field is required by Zuora but it is not required by Adyen. To create the ACH payment method successfully, specify a real value for this field if you can. If it is not possible to get the real value for it, specify a dummy value. .</param>
        /// <param name="cardNumber">The masked credit card number.  When &#x60;cardNumber&#x60; is &#x60;null&#x60;, the following fields will not be returned:   - &#x60;expirationMonth&#x60;   - &#x60;expirationYear&#x60;   - &#x60;accountHolderInfo&#x60; .</param>
        /// <param name="expirationMonth">One or two digits expiration month (1-12).          .</param>
        /// <param name="expirationYear">Four-digit expiration year. .</param>
        /// <param name="securityCode">The CVV or CVV2 security code for the credit card or debit card.             Only required if changing expirationMonth, expirationYear, or cardHolderName.             To ensure PCI compliance, this value isn&#39;&#39;t stored and can&#39;&#39;t be queried.                   .</param>
        /// <param name="bAID">ID of a PayPal billing agreement. For example, I-1TJ3GAGG82Y9. .</param>
        /// <param name="email">Email address associated with the PayPal payment method.  .</param>
        /// <param name="preapprovalKey">The PayPal preapproval key. .</param>
        public GETPaymentMethodResponse(GETPMAccountHolderInfo accountHolderInfo = default(GETPMAccountHolderInfo), string bankIdentificationNumber = default(string), string createdBy = default(string), DateTime createdOn = default(DateTime), string creditCardMaskNumber = default(string), string creditCardType = default(string), string deviceSessionId = default(string), ExistingMandateEnum? existingMandate = default(ExistingMandateEnum?), string id = default(string), string ipAddress = default(string), bool isDefault = default(bool), DateTime lastFailedSaleTransactionDate = default(DateTime), string lastTransaction = default(string), DateTime lastTransactionTime = default(DateTime), POSTPMMandateInfo mandateInfo = default(POSTPMMandateInfo), int maxConsecutivePaymentFailures = default(int), int numConsecutiveFailures = default(int), int paymentRetryWindow = default(int), string secondTokenId = default(string), StatusEnum? status = default(StatusEnum?), string tokenId = default(string), int totalNumberOfErrorPayments = default(int), int totalNumberOfProcessedPayments = default(int), string type = default(string), string updatedBy = default(string), DateTime updatedOn = default(DateTime), bool useDefaultRetryRule = default(bool), string iBAN = default(string), string accountNumber = default(string), string bankCode = default(string), string bankTransferType = default(string), string branchCode = default(string), string businessIdentificationCode = default(string), string identityNumber = default(string), string bankABACode = default(string), string bankAccountName = default(string), string bankAccountNumber = default(string), BankAccountTypeEnum? bankAccountType = default(BankAccountTypeEnum?), string bankName = default(string), string cardNumber = default(string), int expirationMonth = default(int), int expirationYear = default(int), string securityCode = default(string), string bAID = default(string), string email = default(string), string preapprovalKey = default(string))
        {
            this.AccountHolderInfo = accountHolderInfo;
            this.BankIdentificationNumber = bankIdentificationNumber;
            this.CreatedBy = createdBy;
            this.CreatedOn = createdOn;
            this.CreditCardMaskNumber = creditCardMaskNumber;
            this.CreditCardType = creditCardType;
            this.DeviceSessionId = deviceSessionId;
            this.ExistingMandate = existingMandate;
            this.Id = id;
            this.IpAddress = ipAddress;
            this.IsDefault = isDefault;
            this.LastFailedSaleTransactionDate = lastFailedSaleTransactionDate;
            this.LastTransaction = lastTransaction;
            this.LastTransactionTime = lastTransactionTime;
            this.MandateInfo = mandateInfo;
            this.MaxConsecutivePaymentFailures = maxConsecutivePaymentFailures;
            this.NumConsecutiveFailures = numConsecutiveFailures;
            this.PaymentRetryWindow = paymentRetryWindow;
            this.SecondTokenId = secondTokenId;
            this.Status = status;
            this.TokenId = tokenId;
            this.TotalNumberOfErrorPayments = totalNumberOfErrorPayments;
            this.TotalNumberOfProcessedPayments = totalNumberOfProcessedPayments;
            this.Type = type;
            this.UpdatedBy = updatedBy;
            this.UpdatedOn = updatedOn;
            this.UseDefaultRetryRule = useDefaultRetryRule;
            this.IBAN = iBAN;
            this.AccountNumber = accountNumber;
            this.BankCode = bankCode;
            this.BankTransferType = bankTransferType;
            this.BranchCode = branchCode;
            this.BusinessIdentificationCode = businessIdentificationCode;
            this.IdentityNumber = identityNumber;
            this.BankABACode = bankABACode;
            this.BankAccountName = bankAccountName;
            this.BankAccountNumber = bankAccountNumber;
            this.BankAccountType = bankAccountType;
            this.BankName = bankName;
            this.CardNumber = cardNumber;
            this.ExpirationMonth = expirationMonth;
            this.ExpirationYear = expirationYear;
            this.SecurityCode = securityCode;
            this.BAID = bAID;
            this.Email = email;
            this.PreapprovalKey = preapprovalKey;
        }

        /// <summary>
        /// Gets or Sets AccountHolderInfo
        /// </summary>
        [DataMember(Name = "accountHolderInfo", EmitDefaultValue = false)]
        public GETPMAccountHolderInfo AccountHolderInfo { get; set; }

        /// <summary>
        /// The first six or eight digits of the payment method&#39;s number, such as the credit card number or account number. Banks use this number to identify a payment method. 
        /// </summary>
        /// <value>The first six or eight digits of the payment method&#39;s number, such as the credit card number or account number. Banks use this number to identify a payment method. </value>
        [DataMember(Name = "bankIdentificationNumber", EmitDefaultValue = false)]
        public string BankIdentificationNumber { get; set; }

        /// <summary>
        /// ID of the user who created this payment method.
        /// </summary>
        /// <value>ID of the user who created this payment method.</value>
        [DataMember(Name = "createdBy", EmitDefaultValue = false)]
        public string CreatedBy { get; set; }

        /// <summary>
        /// The date and time when the payment method was created, in &#x60;yyyy-mm-dd hh:mm:ss&#x60; format. 
        /// </summary>
        /// <value>The date and time when the payment method was created, in &#x60;yyyy-mm-dd hh:mm:ss&#x60; format. </value>
        [DataMember(Name = "createdOn", EmitDefaultValue = false)]
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// The masked credit card number, such as: &#x60;&#x60;&#x60; *********1112 &#x60;&#x60;&#x60; **Note:** This field is only returned for Credit Card Reference Transaction payment type. 
        /// </summary>
        /// <value>The masked credit card number, such as: &#x60;&#x60;&#x60; *********1112 &#x60;&#x60;&#x60; **Note:** This field is only returned for Credit Card Reference Transaction payment type. </value>
        [DataMember(Name = "creditCardMaskNumber", EmitDefaultValue = false)]
        public string CreditCardMaskNumber { get; set; }

        /// <summary>
        /// The type of the credit card or debit card.  Possible values include &#x60;Visa&#x60;, &#x60;MasterCard&#x60;, &#x60;AmericanExpress&#x60;, &#x60;Discover&#x60;, &#x60;JCB&#x60;, and &#x60;Diners&#x60;. For more information about credit card types supported by different payment gateways, see [Supported Payment Gateways](https://knowledgecenter.zuora.com/CB_Billing/M_Payment_Gateways/Supported_Payment_Gateways).  **Note:** This field is only returned for the Credit Card and Debit Card payment types. 
        /// </summary>
        /// <value>The type of the credit card or debit card.  Possible values include &#x60;Visa&#x60;, &#x60;MasterCard&#x60;, &#x60;AmericanExpress&#x60;, &#x60;Discover&#x60;, &#x60;JCB&#x60;, and &#x60;Diners&#x60;. For more information about credit card types supported by different payment gateways, see [Supported Payment Gateways](https://knowledgecenter.zuora.com/CB_Billing/M_Payment_Gateways/Supported_Payment_Gateways).  **Note:** This field is only returned for the Credit Card and Debit Card payment types. </value>
        [DataMember(Name = "creditCardType", EmitDefaultValue = false)]
        public string CreditCardType { get; set; }

        /// <summary>
        /// The session ID of the user when the &#x60;PaymentMethod&#x60; was created or updated. 
        /// </summary>
        /// <value>The session ID of the user when the &#x60;PaymentMethod&#x60; was created or updated. </value>
        [DataMember(Name = "deviceSessionId", EmitDefaultValue = false)]
        public string DeviceSessionId { get; set; }

        /// <summary>
        /// The payment method ID. 
        /// </summary>
        /// <value>The payment method ID. </value>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }

        /// <summary>
        /// The IP address of the user when the payment method was created or updated. 
        /// </summary>
        /// <value>The IP address of the user when the payment method was created or updated. </value>
        [DataMember(Name = "ipAddress", EmitDefaultValue = false)]
        public string IpAddress { get; set; }

        /// <summary>
        /// Indicates whether this payment method is the default payment method for the account. 
        /// </summary>
        /// <value>Indicates whether this payment method is the default payment method for the account. </value>
        [DataMember(Name = "isDefault", EmitDefaultValue = true)]
        public bool IsDefault { get; set; }

        /// <summary>
        /// The date of the last failed attempt to collect payment with this payment method. 
        /// </summary>
        /// <value>The date of the last failed attempt to collect payment with this payment method. </value>
        [DataMember(Name = "lastFailedSaleTransactionDate", EmitDefaultValue = false)]
        public DateTime LastFailedSaleTransactionDate { get; set; }

        /// <summary>
        /// ID of the last transaction of this payment method.
        /// </summary>
        /// <value>ID of the last transaction of this payment method.</value>
        [DataMember(Name = "lastTransaction", EmitDefaultValue = false)]
        public string LastTransaction { get; set; }

        /// <summary>
        /// The time when the last transaction of this payment method happened.
        /// </summary>
        /// <value>The time when the last transaction of this payment method happened.</value>
        [DataMember(Name = "lastTransactionTime", EmitDefaultValue = false)]
        public DateTime LastTransactionTime { get; set; }

        /// <summary>
        /// Gets or Sets MandateInfo
        /// </summary>
        [DataMember(Name = "mandateInfo", EmitDefaultValue = false)]
        public POSTPMMandateInfo MandateInfo { get; set; }

        /// <summary>
        /// The number of allowable consecutive failures Zuora attempts with the payment method before stopping. 
        /// </summary>
        /// <value>The number of allowable consecutive failures Zuora attempts with the payment method before stopping. </value>
        [DataMember(Name = "maxConsecutivePaymentFailures", EmitDefaultValue = false)]
        public int MaxConsecutivePaymentFailures { get; set; }

        /// <summary>
        /// The number of consecutive failed payments for this payment method. It is reset to &#x60;0&#x60; upon successful payment.  
        /// </summary>
        /// <value>The number of consecutive failed payments for this payment method. It is reset to &#x60;0&#x60; upon successful payment.  </value>
        [DataMember(Name = "numConsecutiveFailures", EmitDefaultValue = false)]
        public int NumConsecutiveFailures { get; set; }

        /// <summary>
        /// The retry interval setting, which prevents making a payment attempt if the last failed attempt was within the last specified number of hours. 
        /// </summary>
        /// <value>The retry interval setting, which prevents making a payment attempt if the last failed attempt was within the last specified number of hours. </value>
        [DataMember(Name = "paymentRetryWindow", EmitDefaultValue = false)]
        public int PaymentRetryWindow { get; set; }

        /// <summary>
        /// A gateway unique identifier that replaces sensitive payment method data.  **Note:** This field is only returned for the Credit Card Reference Transaction payment type. 
        /// </summary>
        /// <value>A gateway unique identifier that replaces sensitive payment method data.  **Note:** This field is only returned for the Credit Card Reference Transaction payment type. </value>
        [DataMember(Name = "secondTokenId", EmitDefaultValue = false)]
        public string SecondTokenId { get; set; }

        /// <summary>
        /// A gateway unique identifier that replaces sensitive payment method data or represents a gateway&#39;s unique customer profile.  **Note:** This field is only returned for the Credit Card Reference Transaction payment type. 
        /// </summary>
        /// <value>A gateway unique identifier that replaces sensitive payment method data or represents a gateway&#39;s unique customer profile.  **Note:** This field is only returned for the Credit Card Reference Transaction payment type. </value>
        [DataMember(Name = "tokenId", EmitDefaultValue = false)]
        public string TokenId { get; set; }

        /// <summary>
        /// The number of error payments that used this payment method. 
        /// </summary>
        /// <value>The number of error payments that used this payment method. </value>
        [DataMember(Name = "totalNumberOfErrorPayments", EmitDefaultValue = false)]
        public int TotalNumberOfErrorPayments { get; set; }

        /// <summary>
        /// The number of successful payments that used this payment method. 
        /// </summary>
        /// <value>The number of successful payments that used this payment method. </value>
        [DataMember(Name = "totalNumberOfProcessedPayments", EmitDefaultValue = false)]
        public int TotalNumberOfProcessedPayments { get; set; }

        /// <summary>
        /// The type of the payment method. For example, &#x60;CreditCard&#x60;. 
        /// </summary>
        /// <value>The type of the payment method. For example, &#x60;CreditCard&#x60;. </value>
        [DataMember(Name = "type", EmitDefaultValue = false)]
        public string Type { get; set; }

        /// <summary>
        /// ID of the user who made the last update to this payment method.
        /// </summary>
        /// <value>ID of the user who made the last update to this payment method.</value>
        [DataMember(Name = "updatedBy", EmitDefaultValue = false)]
        public string UpdatedBy { get; set; }

        /// <summary>
        /// The last date and time when the payment method was updated, in &#x60;yyyy-mm-dd hh:mm:ss&#x60; format. 
        /// </summary>
        /// <value>The last date and time when the payment method was updated, in &#x60;yyyy-mm-dd hh:mm:ss&#x60; format. </value>
        [DataMember(Name = "updatedOn", EmitDefaultValue = false)]
        public DateTime UpdatedOn { get; set; }

        /// <summary>
        /// Indicates whether this payment method uses the default retry rules configured in the Zuora Payments settings. 
        /// </summary>
        /// <value>Indicates whether this payment method uses the default retry rules configured in the Zuora Payments settings. </value>
        [DataMember(Name = "useDefaultRetryRule", EmitDefaultValue = true)]
        public bool UseDefaultRetryRule { get; set; }

        /// <summary>
        /// The International Bank Account Number used to create the SEPA payment method. The value is masked. 
        /// </summary>
        /// <value>The International Bank Account Number used to create the SEPA payment method. The value is masked. </value>
        [DataMember(Name = "IBAN", EmitDefaultValue = false)]
        public string IBAN { get; set; }

        /// <summary>
        /// The number of the customer&#39;s bank account and it is masked. 
        /// </summary>
        /// <value>The number of the customer&#39;s bank account and it is masked. </value>
        [DataMember(Name = "accountNumber", EmitDefaultValue = false)]
        public string AccountNumber { get; set; }

        /// <summary>
        /// The sort code or number that identifies the bank. This is also known as the sort code.          
        /// </summary>
        /// <value>The sort code or number that identifies the bank. This is also known as the sort code.          </value>
        [DataMember(Name = "bankCode", EmitDefaultValue = false)]
        public string BankCode { get; set; }

        /// <summary>
        /// The type of the Bank Transfer payment method. For example, &#x60;SEPA&#x60;. 
        /// </summary>
        /// <value>The type of the Bank Transfer payment method. For example, &#x60;SEPA&#x60;. </value>
        [DataMember(Name = "bankTransferType", EmitDefaultValue = false)]
        public string BankTransferType { get; set; }

        /// <summary>
        /// The branch code of the bank used for Direct Debit.           
        /// </summary>
        /// <value>The branch code of the bank used for Direct Debit.           </value>
        [DataMember(Name = "branchCode", EmitDefaultValue = false)]
        public string BranchCode { get; set; }

        /// <summary>
        /// The BIC code used for SEPA. The value is masked.        
        /// </summary>
        /// <value>The BIC code used for SEPA. The value is masked.        </value>
        [DataMember(Name = "businessIdentificationCode", EmitDefaultValue = false)]
        public string BusinessIdentificationCode { get; set; }

        /// <summary>
        /// The identity number used for Bank Transfer.         
        /// </summary>
        /// <value>The identity number used for Bank Transfer.         </value>
        [DataMember(Name = "identityNumber", EmitDefaultValue = false)]
        public string IdentityNumber { get; set; }

        /// <summary>
        /// The nine-digit routing number or ABA number used by banks. This field is only required if the &#x60;type&#x60; field is set to &#x60;ACH&#x60;. 
        /// </summary>
        /// <value>The nine-digit routing number or ABA number used by banks. This field is only required if the &#x60;type&#x60; field is set to &#x60;ACH&#x60;. </value>
        [DataMember(Name = "bankABACode", EmitDefaultValue = false)]
        public string BankABACode { get; set; }

        /// <summary>
        /// The name of the account holder, which can be either a person or a company. This field is only required if the &#x60;type&#x60; field is set to &#x60;ACH&#x60;. 
        /// </summary>
        /// <value>The name of the account holder, which can be either a person or a company. This field is only required if the &#x60;type&#x60; field is set to &#x60;ACH&#x60;. </value>
        [DataMember(Name = "bankAccountName", EmitDefaultValue = false)]
        public string BankAccountName { get; set; }

        /// <summary>
        /// The bank account number associated with the ACH payment. This field is only required if the &#x60;type&#x60; field is set to &#x60;ACH&#x60;. 
        /// </summary>
        /// <value>The bank account number associated with the ACH payment. This field is only required if the &#x60;type&#x60; field is set to &#x60;ACH&#x60;. </value>
        [DataMember(Name = "bankAccountNumber", EmitDefaultValue = false)]
        public string BankAccountNumber { get; set; }

        /// <summary>
        /// The name of the bank where the ACH payment account is held. This field is only required if the &#x60;type&#x60; field is set to &#x60;ACH&#x60;.  When creating an ACH payment method on Adyen, this field is required by Zuora but it is not required by Adyen. To create the ACH payment method successfully, specify a real value for this field if you can. If it is not possible to get the real value for it, specify a dummy value. 
        /// </summary>
        /// <value>The name of the bank where the ACH payment account is held. This field is only required if the &#x60;type&#x60; field is set to &#x60;ACH&#x60;.  When creating an ACH payment method on Adyen, this field is required by Zuora but it is not required by Adyen. To create the ACH payment method successfully, specify a real value for this field if you can. If it is not possible to get the real value for it, specify a dummy value. </value>
        [DataMember(Name = "bankName", EmitDefaultValue = false)]
        public string BankName { get; set; }

        /// <summary>
        /// The masked credit card number.  When &#x60;cardNumber&#x60; is &#x60;null&#x60;, the following fields will not be returned:   - &#x60;expirationMonth&#x60;   - &#x60;expirationYear&#x60;   - &#x60;accountHolderInfo&#x60; 
        /// </summary>
        /// <value>The masked credit card number.  When &#x60;cardNumber&#x60; is &#x60;null&#x60;, the following fields will not be returned:   - &#x60;expirationMonth&#x60;   - &#x60;expirationYear&#x60;   - &#x60;accountHolderInfo&#x60; </value>
        [DataMember(Name = "cardNumber", EmitDefaultValue = false)]
        public string CardNumber { get; set; }

        /// <summary>
        /// One or two digits expiration month (1-12).          
        /// </summary>
        /// <value>One or two digits expiration month (1-12).          </value>
        [DataMember(Name = "expirationMonth", EmitDefaultValue = false)]
        public int ExpirationMonth { get; set; }

        /// <summary>
        /// Four-digit expiration year. 
        /// </summary>
        /// <value>Four-digit expiration year. </value>
        [DataMember(Name = "expirationYear", EmitDefaultValue = false)]
        public int ExpirationYear { get; set; }

        /// <summary>
        /// The CVV or CVV2 security code for the credit card or debit card.             Only required if changing expirationMonth, expirationYear, or cardHolderName.             To ensure PCI compliance, this value isn&#39;&#39;t stored and can&#39;&#39;t be queried.                   
        /// </summary>
        /// <value>The CVV or CVV2 security code for the credit card or debit card.             Only required if changing expirationMonth, expirationYear, or cardHolderName.             To ensure PCI compliance, this value isn&#39;&#39;t stored and can&#39;&#39;t be queried.                   </value>
        [DataMember(Name = "securityCode", EmitDefaultValue = false)]
        public string SecurityCode { get; set; }

        /// <summary>
        /// ID of a PayPal billing agreement. For example, I-1TJ3GAGG82Y9. 
        /// </summary>
        /// <value>ID of a PayPal billing agreement. For example, I-1TJ3GAGG82Y9. </value>
        [DataMember(Name = "BAID", EmitDefaultValue = false)]
        public string BAID { get; set; }

        /// <summary>
        /// Email address associated with the PayPal payment method.  
        /// </summary>
        /// <value>Email address associated with the PayPal payment method.  </value>
        [DataMember(Name = "email", EmitDefaultValue = false)]
        public string Email { get; set; }

        /// <summary>
        /// The PayPal preapproval key. 
        /// </summary>
        /// <value>The PayPal preapproval key. </value>
        [DataMember(Name = "preapprovalKey", EmitDefaultValue = false)]
        public string PreapprovalKey { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class GETPaymentMethodResponse {\n");
            sb.Append("  AccountHolderInfo: ").Append(AccountHolderInfo).Append("\n");
            sb.Append("  BankIdentificationNumber: ").Append(BankIdentificationNumber).Append("\n");
            sb.Append("  CreatedBy: ").Append(CreatedBy).Append("\n");
            sb.Append("  CreatedOn: ").Append(CreatedOn).Append("\n");
            sb.Append("  CreditCardMaskNumber: ").Append(CreditCardMaskNumber).Append("\n");
            sb.Append("  CreditCardType: ").Append(CreditCardType).Append("\n");
            sb.Append("  DeviceSessionId: ").Append(DeviceSessionId).Append("\n");
            sb.Append("  ExistingMandate: ").Append(ExistingMandate).Append("\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  IpAddress: ").Append(IpAddress).Append("\n");
            sb.Append("  IsDefault: ").Append(IsDefault).Append("\n");
            sb.Append("  LastFailedSaleTransactionDate: ").Append(LastFailedSaleTransactionDate).Append("\n");
            sb.Append("  LastTransaction: ").Append(LastTransaction).Append("\n");
            sb.Append("  LastTransactionTime: ").Append(LastTransactionTime).Append("\n");
            sb.Append("  MandateInfo: ").Append(MandateInfo).Append("\n");
            sb.Append("  MaxConsecutivePaymentFailures: ").Append(MaxConsecutivePaymentFailures).Append("\n");
            sb.Append("  NumConsecutiveFailures: ").Append(NumConsecutiveFailures).Append("\n");
            sb.Append("  PaymentRetryWindow: ").Append(PaymentRetryWindow).Append("\n");
            sb.Append("  SecondTokenId: ").Append(SecondTokenId).Append("\n");
            sb.Append("  Status: ").Append(Status).Append("\n");
            sb.Append("  TokenId: ").Append(TokenId).Append("\n");
            sb.Append("  TotalNumberOfErrorPayments: ").Append(TotalNumberOfErrorPayments).Append("\n");
            sb.Append("  TotalNumberOfProcessedPayments: ").Append(TotalNumberOfProcessedPayments).Append("\n");
            sb.Append("  Type: ").Append(Type).Append("\n");
            sb.Append("  UpdatedBy: ").Append(UpdatedBy).Append("\n");
            sb.Append("  UpdatedOn: ").Append(UpdatedOn).Append("\n");
            sb.Append("  UseDefaultRetryRule: ").Append(UseDefaultRetryRule).Append("\n");
            sb.Append("  IBAN: ").Append(IBAN).Append("\n");
            sb.Append("  AccountNumber: ").Append(AccountNumber).Append("\n");
            sb.Append("  BankCode: ").Append(BankCode).Append("\n");
            sb.Append("  BankTransferType: ").Append(BankTransferType).Append("\n");
            sb.Append("  BranchCode: ").Append(BranchCode).Append("\n");
            sb.Append("  BusinessIdentificationCode: ").Append(BusinessIdentificationCode).Append("\n");
            sb.Append("  IdentityNumber: ").Append(IdentityNumber).Append("\n");
            sb.Append("  BankABACode: ").Append(BankABACode).Append("\n");
            sb.Append("  BankAccountName: ").Append(BankAccountName).Append("\n");
            sb.Append("  BankAccountNumber: ").Append(BankAccountNumber).Append("\n");
            sb.Append("  BankAccountType: ").Append(BankAccountType).Append("\n");
            sb.Append("  BankName: ").Append(BankName).Append("\n");
            sb.Append("  CardNumber: ").Append(CardNumber).Append("\n");
            sb.Append("  ExpirationMonth: ").Append(ExpirationMonth).Append("\n");
            sb.Append("  ExpirationYear: ").Append(ExpirationYear).Append("\n");
            sb.Append("  SecurityCode: ").Append(SecurityCode).Append("\n");
            sb.Append("  BAID: ").Append(BAID).Append("\n");
            sb.Append("  Email: ").Append(Email).Append("\n");
            sb.Append("  PreapprovalKey: ").Append(PreapprovalKey).Append("\n");
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
            return this.Equals(input as GETPaymentMethodResponse);
        }

        /// <summary>
        /// Returns true if GETPaymentMethodResponse instances are equal
        /// </summary>
        /// <param name="input">Instance of GETPaymentMethodResponse to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(GETPaymentMethodResponse input)
        {
            if (input == null)
            {
                return false;
            }
            return 
                (
                    this.AccountHolderInfo == input.AccountHolderInfo ||
                    (this.AccountHolderInfo != null &&
                    this.AccountHolderInfo.Equals(input.AccountHolderInfo))
                ) && 
                (
                    this.BankIdentificationNumber == input.BankIdentificationNumber ||
                    (this.BankIdentificationNumber != null &&
                    this.BankIdentificationNumber.Equals(input.BankIdentificationNumber))
                ) && 
                (
                    this.CreatedBy == input.CreatedBy ||
                    (this.CreatedBy != null &&
                    this.CreatedBy.Equals(input.CreatedBy))
                ) && 
                (
                    this.CreatedOn == input.CreatedOn ||
                    (this.CreatedOn != null &&
                    this.CreatedOn.Equals(input.CreatedOn))
                ) && 
                (
                    this.CreditCardMaskNumber == input.CreditCardMaskNumber ||
                    (this.CreditCardMaskNumber != null &&
                    this.CreditCardMaskNumber.Equals(input.CreditCardMaskNumber))
                ) && 
                (
                    this.CreditCardType == input.CreditCardType ||
                    (this.CreditCardType != null &&
                    this.CreditCardType.Equals(input.CreditCardType))
                ) && 
                (
                    this.DeviceSessionId == input.DeviceSessionId ||
                    (this.DeviceSessionId != null &&
                    this.DeviceSessionId.Equals(input.DeviceSessionId))
                ) && 
                (
                    this.ExistingMandate == input.ExistingMandate ||
                    this.ExistingMandate.Equals(input.ExistingMandate)
                ) && 
                (
                    this.Id == input.Id ||
                    (this.Id != null &&
                    this.Id.Equals(input.Id))
                ) && 
                (
                    this.IpAddress == input.IpAddress ||
                    (this.IpAddress != null &&
                    this.IpAddress.Equals(input.IpAddress))
                ) && 
                (
                    this.IsDefault == input.IsDefault ||
                    this.IsDefault.Equals(input.IsDefault)
                ) && 
                (
                    this.LastFailedSaleTransactionDate == input.LastFailedSaleTransactionDate ||
                    (this.LastFailedSaleTransactionDate != null &&
                    this.LastFailedSaleTransactionDate.Equals(input.LastFailedSaleTransactionDate))
                ) && 
                (
                    this.LastTransaction == input.LastTransaction ||
                    (this.LastTransaction != null &&
                    this.LastTransaction.Equals(input.LastTransaction))
                ) && 
                (
                    this.LastTransactionTime == input.LastTransactionTime ||
                    (this.LastTransactionTime != null &&
                    this.LastTransactionTime.Equals(input.LastTransactionTime))
                ) && 
                (
                    this.MandateInfo == input.MandateInfo ||
                    (this.MandateInfo != null &&
                    this.MandateInfo.Equals(input.MandateInfo))
                ) && 
                (
                    this.MaxConsecutivePaymentFailures == input.MaxConsecutivePaymentFailures ||
                    this.MaxConsecutivePaymentFailures.Equals(input.MaxConsecutivePaymentFailures)
                ) && 
                (
                    this.NumConsecutiveFailures == input.NumConsecutiveFailures ||
                    this.NumConsecutiveFailures.Equals(input.NumConsecutiveFailures)
                ) && 
                (
                    this.PaymentRetryWindow == input.PaymentRetryWindow ||
                    this.PaymentRetryWindow.Equals(input.PaymentRetryWindow)
                ) && 
                (
                    this.SecondTokenId == input.SecondTokenId ||
                    (this.SecondTokenId != null &&
                    this.SecondTokenId.Equals(input.SecondTokenId))
                ) && 
                (
                    this.Status == input.Status ||
                    this.Status.Equals(input.Status)
                ) && 
                (
                    this.TokenId == input.TokenId ||
                    (this.TokenId != null &&
                    this.TokenId.Equals(input.TokenId))
                ) && 
                (
                    this.TotalNumberOfErrorPayments == input.TotalNumberOfErrorPayments ||
                    this.TotalNumberOfErrorPayments.Equals(input.TotalNumberOfErrorPayments)
                ) && 
                (
                    this.TotalNumberOfProcessedPayments == input.TotalNumberOfProcessedPayments ||
                    this.TotalNumberOfProcessedPayments.Equals(input.TotalNumberOfProcessedPayments)
                ) && 
                (
                    this.Type == input.Type ||
                    (this.Type != null &&
                    this.Type.Equals(input.Type))
                ) && 
                (
                    this.UpdatedBy == input.UpdatedBy ||
                    (this.UpdatedBy != null &&
                    this.UpdatedBy.Equals(input.UpdatedBy))
                ) && 
                (
                    this.UpdatedOn == input.UpdatedOn ||
                    (this.UpdatedOn != null &&
                    this.UpdatedOn.Equals(input.UpdatedOn))
                ) && 
                (
                    this.UseDefaultRetryRule == input.UseDefaultRetryRule ||
                    this.UseDefaultRetryRule.Equals(input.UseDefaultRetryRule)
                ) && 
                (
                    this.IBAN == input.IBAN ||
                    (this.IBAN != null &&
                    this.IBAN.Equals(input.IBAN))
                ) && 
                (
                    this.AccountNumber == input.AccountNumber ||
                    (this.AccountNumber != null &&
                    this.AccountNumber.Equals(input.AccountNumber))
                ) && 
                (
                    this.BankCode == input.BankCode ||
                    (this.BankCode != null &&
                    this.BankCode.Equals(input.BankCode))
                ) && 
                (
                    this.BankTransferType == input.BankTransferType ||
                    (this.BankTransferType != null &&
                    this.BankTransferType.Equals(input.BankTransferType))
                ) && 
                (
                    this.BranchCode == input.BranchCode ||
                    (this.BranchCode != null &&
                    this.BranchCode.Equals(input.BranchCode))
                ) && 
                (
                    this.BusinessIdentificationCode == input.BusinessIdentificationCode ||
                    (this.BusinessIdentificationCode != null &&
                    this.BusinessIdentificationCode.Equals(input.BusinessIdentificationCode))
                ) && 
                (
                    this.IdentityNumber == input.IdentityNumber ||
                    (this.IdentityNumber != null &&
                    this.IdentityNumber.Equals(input.IdentityNumber))
                ) && 
                (
                    this.BankABACode == input.BankABACode ||
                    (this.BankABACode != null &&
                    this.BankABACode.Equals(input.BankABACode))
                ) && 
                (
                    this.BankAccountName == input.BankAccountName ||
                    (this.BankAccountName != null &&
                    this.BankAccountName.Equals(input.BankAccountName))
                ) && 
                (
                    this.BankAccountNumber == input.BankAccountNumber ||
                    (this.BankAccountNumber != null &&
                    this.BankAccountNumber.Equals(input.BankAccountNumber))
                ) && 
                (
                    this.BankAccountType == input.BankAccountType ||
                    this.BankAccountType.Equals(input.BankAccountType)
                ) && 
                (
                    this.BankName == input.BankName ||
                    (this.BankName != null &&
                    this.BankName.Equals(input.BankName))
                ) && 
                (
                    this.CardNumber == input.CardNumber ||
                    (this.CardNumber != null &&
                    this.CardNumber.Equals(input.CardNumber))
                ) && 
                (
                    this.ExpirationMonth == input.ExpirationMonth ||
                    this.ExpirationMonth.Equals(input.ExpirationMonth)
                ) && 
                (
                    this.ExpirationYear == input.ExpirationYear ||
                    this.ExpirationYear.Equals(input.ExpirationYear)
                ) && 
                (
                    this.SecurityCode == input.SecurityCode ||
                    (this.SecurityCode != null &&
                    this.SecurityCode.Equals(input.SecurityCode))
                ) && 
                (
                    this.BAID == input.BAID ||
                    (this.BAID != null &&
                    this.BAID.Equals(input.BAID))
                ) && 
                (
                    this.Email == input.Email ||
                    (this.Email != null &&
                    this.Email.Equals(input.Email))
                ) && 
                (
                    this.PreapprovalKey == input.PreapprovalKey ||
                    (this.PreapprovalKey != null &&
                    this.PreapprovalKey.Equals(input.PreapprovalKey))
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
                if (this.AccountHolderInfo != null)
                {
                    hashCode = (hashCode * 59) + this.AccountHolderInfo.GetHashCode();
                }
                if (this.BankIdentificationNumber != null)
                {
                    hashCode = (hashCode * 59) + this.BankIdentificationNumber.GetHashCode();
                }
                if (this.CreatedBy != null)
                {
                    hashCode = (hashCode * 59) + this.CreatedBy.GetHashCode();
                }
                if (this.CreatedOn != null)
                {
                    hashCode = (hashCode * 59) + this.CreatedOn.GetHashCode();
                }
                if (this.CreditCardMaskNumber != null)
                {
                    hashCode = (hashCode * 59) + this.CreditCardMaskNumber.GetHashCode();
                }
                if (this.CreditCardType != null)
                {
                    hashCode = (hashCode * 59) + this.CreditCardType.GetHashCode();
                }
                if (this.DeviceSessionId != null)
                {
                    hashCode = (hashCode * 59) + this.DeviceSessionId.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.ExistingMandate.GetHashCode();
                if (this.Id != null)
                {
                    hashCode = (hashCode * 59) + this.Id.GetHashCode();
                }
                if (this.IpAddress != null)
                {
                    hashCode = (hashCode * 59) + this.IpAddress.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.IsDefault.GetHashCode();
                if (this.LastFailedSaleTransactionDate != null)
                {
                    hashCode = (hashCode * 59) + this.LastFailedSaleTransactionDate.GetHashCode();
                }
                if (this.LastTransaction != null)
                {
                    hashCode = (hashCode * 59) + this.LastTransaction.GetHashCode();
                }
                if (this.LastTransactionTime != null)
                {
                    hashCode = (hashCode * 59) + this.LastTransactionTime.GetHashCode();
                }
                if (this.MandateInfo != null)
                {
                    hashCode = (hashCode * 59) + this.MandateInfo.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.MaxConsecutivePaymentFailures.GetHashCode();
                hashCode = (hashCode * 59) + this.NumConsecutiveFailures.GetHashCode();
                hashCode = (hashCode * 59) + this.PaymentRetryWindow.GetHashCode();
                if (this.SecondTokenId != null)
                {
                    hashCode = (hashCode * 59) + this.SecondTokenId.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.Status.GetHashCode();
                if (this.TokenId != null)
                {
                    hashCode = (hashCode * 59) + this.TokenId.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.TotalNumberOfErrorPayments.GetHashCode();
                hashCode = (hashCode * 59) + this.TotalNumberOfProcessedPayments.GetHashCode();
                if (this.Type != null)
                {
                    hashCode = (hashCode * 59) + this.Type.GetHashCode();
                }
                if (this.UpdatedBy != null)
                {
                    hashCode = (hashCode * 59) + this.UpdatedBy.GetHashCode();
                }
                if (this.UpdatedOn != null)
                {
                    hashCode = (hashCode * 59) + this.UpdatedOn.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.UseDefaultRetryRule.GetHashCode();
                if (this.IBAN != null)
                {
                    hashCode = (hashCode * 59) + this.IBAN.GetHashCode();
                }
                if (this.AccountNumber != null)
                {
                    hashCode = (hashCode * 59) + this.AccountNumber.GetHashCode();
                }
                if (this.BankCode != null)
                {
                    hashCode = (hashCode * 59) + this.BankCode.GetHashCode();
                }
                if (this.BankTransferType != null)
                {
                    hashCode = (hashCode * 59) + this.BankTransferType.GetHashCode();
                }
                if (this.BranchCode != null)
                {
                    hashCode = (hashCode * 59) + this.BranchCode.GetHashCode();
                }
                if (this.BusinessIdentificationCode != null)
                {
                    hashCode = (hashCode * 59) + this.BusinessIdentificationCode.GetHashCode();
                }
                if (this.IdentityNumber != null)
                {
                    hashCode = (hashCode * 59) + this.IdentityNumber.GetHashCode();
                }
                if (this.BankABACode != null)
                {
                    hashCode = (hashCode * 59) + this.BankABACode.GetHashCode();
                }
                if (this.BankAccountName != null)
                {
                    hashCode = (hashCode * 59) + this.BankAccountName.GetHashCode();
                }
                if (this.BankAccountNumber != null)
                {
                    hashCode = (hashCode * 59) + this.BankAccountNumber.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.BankAccountType.GetHashCode();
                if (this.BankName != null)
                {
                    hashCode = (hashCode * 59) + this.BankName.GetHashCode();
                }
                if (this.CardNumber != null)
                {
                    hashCode = (hashCode * 59) + this.CardNumber.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.ExpirationMonth.GetHashCode();
                hashCode = (hashCode * 59) + this.ExpirationYear.GetHashCode();
                if (this.SecurityCode != null)
                {
                    hashCode = (hashCode * 59) + this.SecurityCode.GetHashCode();
                }
                if (this.BAID != null)
                {
                    hashCode = (hashCode * 59) + this.BAID.GetHashCode();
                }
                if (this.Email != null)
                {
                    hashCode = (hashCode * 59) + this.Email.GetHashCode();
                }
                if (this.PreapprovalKey != null)
                {
                    hashCode = (hashCode * 59) + this.PreapprovalKey.GetHashCode();
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