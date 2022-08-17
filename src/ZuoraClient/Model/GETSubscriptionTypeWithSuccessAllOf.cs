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
    /// GETSubscriptionTypeWithSuccessAllOf
    /// </summary>
    [DataContract(Name = "GETSubscriptionTypeWithSuccess_allOf")]
    public partial class GETSubscriptionTypeWithSuccessAllOf : IEquatable<GETSubscriptionTypeWithSuccessAllOf>, IValidatableObject
    {
        /// <summary>
        /// An enum field on the Subscription object to indicate the name of a third-party store. This field is used to represent subscriptions created through third-party stores. 
        /// </summary>
        /// <value>An enum field on the Subscription object to indicate the name of a third-party store. This field is used to represent subscriptions created through third-party stores. </value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum ExternallyManagedByEnum
        {
            /// <summary>
            /// Enum Amazon for value: Amazon
            /// </summary>
            [EnumMember(Value = "Amazon")]
            Amazon = 1,

            /// <summary>
            /// Enum Apple for value: Apple
            /// </summary>
            [EnumMember(Value = "Apple")]
            Apple = 2,

            /// <summary>
            /// Enum Google for value: Google
            /// </summary>
            [EnumMember(Value = "Google")]
            Google = 3,

            /// <summary>
            /// Enum Roku for value: Roku
            /// </summary>
            [EnumMember(Value = "Roku")]
            Roku = 4

        }


        /// <summary>
        /// An enum field on the Subscription object to indicate the name of a third-party store. This field is used to represent subscriptions created through third-party stores. 
        /// </summary>
        /// <value>An enum field on the Subscription object to indicate the name of a third-party store. This field is used to represent subscriptions created through third-party stores. </value>
        [DataMember(Name = "externallyManagedBy", EmitDefaultValue = false)]
        public ExternallyManagedByEnum? ExternallyManagedBy { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="GETSubscriptionTypeWithSuccessAllOf" /> class.
        /// </summary>
        /// <param name="accountId">The ID of the account associated with this subscription..</param>
        /// <param name="accountName">The name of the account associated with this subscription..</param>
        /// <param name="accountNumber">The number of the account associated with this subscription..</param>
        /// <param name="autoRenew">If &#x60;true&#x60;, the subscription automatically renews at the end of the term. Default is &#x60;false&#x60;. .</param>
        /// <param name="billToContact">billToContact.</param>
        /// <param name="cancelReason">The reason for a subscription cancellation copied from the &#x60;changeReason&#x60; field of a Cancel Subscription order action.   This field contains valid value only if a subscription is cancelled through the Orders UI or API. Otherwise, the value for this field will always be &#x60;null&#x60;. .</param>
        /// <param name="contractEffectiveDate">Effective contract date for this subscription, as yyyy-mm-dd. .</param>
        /// <param name="contractedMrr">Monthly recurring revenue of the subscription. .</param>
        /// <param name="currentTerm">The length of the period for the current subscription term. .</param>
        /// <param name="currentTermPeriodType">The period type for the current subscription term.  Values are:  * &#x60;Month&#x60; (default) * &#x60;Year&#x60; * &#x60;Day&#x60; * &#x60;Week&#x60; .</param>
        /// <param name="customerAcceptanceDate">The date on which the services or products within a subscription have been accepted by the customer, as yyyy-mm-dd. .</param>
        /// <param name="externallyManagedBy">An enum field on the Subscription object to indicate the name of a third-party store. This field is used to represent subscriptions created through third-party stores. .</param>
        /// <param name="id">Subscription ID. .</param>
        /// <param name="initialTerm">The length of the period for the first subscription term. .</param>
        /// <param name="initialTermPeriodType">The period type for the first subscription term.  Values are:  * &#x60;Month&#x60; (default) * &#x60;Year&#x60; * &#x60;Day&#x60; * &#x60;Week&#x60; .</param>
        /// <param name="invoiceOwnerAccountId">invoiceOwnerAccountId.</param>
        /// <param name="invoiceOwnerAccountName">invoiceOwnerAccountName.</param>
        /// <param name="invoiceOwnerAccountNumber">invoiceOwnerAccountNumber.</param>
        /// <param name="invoiceSeparately">Separates a single subscription from other subscriptions and creates an invoice for the subscription.   If the value is &#x60;true&#x60;, the subscription is billed separately from other subscriptions. If the value is &#x60;false&#x60;, the subscription is included with other subscriptions in the account invoice. .</param>
        /// <param name="isLatestVersion">If &#x60;true&#x60;, the current subscription object is the latest version..</param>
        /// <param name="lastBookingDate">The last booking date of the subscription object. This field is writable only when the subscription is newly created as a first version subscription. You can override the date value when creating a subscription through the Subscribe and Amend API or the subscription creation UI (non-Orders). Otherwise, the default value &#x60;today&#x60; is set per the user&#39;s timezone. The value of this field is as follows: * For a new subscription created by the [Subscribe and Amend APIs](https://knowledgecenter.zuora.com/Billing/Subscriptions/Orders/Orders_Harmonization/Orders_Migration_Guidance#Subscribe_and_Amend_APIs_to_Migrate), this field has the value of the subscription creation date. * For a subscription changed by an amendment, this field has the value of the amendment booking date. * For a subscription created or changed by an order, this field has the value of the order date. .</param>
        /// <param name="notes">A string of up to 65,535 characters. .</param>
        /// <param name="orderNumber">The order number of the order in which the changes on the subscription are made.   **Note:** This field is only available if you have the [Order Metrics](https://knowledgecenter.zuora.com/BC_Subscription_Management/Orders/AA_Overview_of_Orders#Order_Metrics) feature enabled. If you wish to have access to the feature, submit a request at [Zuora Global Support](http://support.zuora.com/). We will investigate your use cases and data before enabling this feature for you. .</param>
        /// <param name="paymentTerm">The name of the payment term associated with the subscription. For example, &#x60;Net 30&#x60;. The payment term determines the due dates of invoices.  **Note**: The value of this field is &#x60;null&#x60; if you have the [Flexible Billing](https://knowledgecenter.zuora.com/Billing/Subscriptions/Flexible_Billing) feature disabled. .</param>
        /// <param name="ratePlans">Container for rate plans. .</param>
        /// <param name="renewalSetting">Specifies whether a termed subscription will remain &#x60;TERMED&#x60; or change to &#x60;EVERGREEN&#x60; when it is renewed.   Values are:  * &#x60;RENEW_WITH_SPECIFIC_TERM&#x60; (default) * &#x60;RENEW_TO_EVERGREEN&#x60; .</param>
        /// <param name="renewalTerm">The length of the period for the subscription renewal term. .</param>
        /// <param name="renewalTermPeriodType">The period type for the subscription renewal term.  Values are:  * &#x60;Month&#x60; (default) * &#x60;Year&#x60; * &#x60;Day&#x60; * &#x60;Week&#x60; .</param>
        /// <param name="revision">An auto-generated decimal value uniquely tagged with a subscription. The value always contains one decimal place, for example, the revision of a new subscription is 1.0. If a further version of the subscription is created, the revision value will be increased by 1. Also, the revision value is always incremental regardless of deletion of subscription versions. .</param>
        /// <param name="serviceActivationDate">The date on which the services or products within a subscription have been activated and access has been provided to the customer, as yyyy-mm-dd .</param>
        /// <param name="status">Subscription status; possible values are:  * &#x60;Draft&#x60; * &#x60;Pending Activation&#x60; * &#x60;Pending Acceptance&#x60; * &#x60;Active&#x60; * &#x60;Cancelled&#x60; * &#x60;Suspended&#x60; .</param>
        /// <param name="subscriptionEndDate">The date when the subscription term ends, where the subscription ends at midnight the day before. For example, if the &#x60;subscriptionEndDate&#x60; is 12/31/2016, the subscriptions ends at midnight (00:00:00 hours) on 12/30/2016. This date is the same as the term end date or the cancelation date, as appropriate. .</param>
        /// <param name="subscriptionNumber">Subscription number..</param>
        /// <param name="subscriptionStartDate">Date the subscription becomes effective. .</param>
        /// <param name="success">Returns &#x60;true&#x60; if the request was processed successfully. .</param>
        /// <param name="termEndDate">Date the subscription term ends. If the subscription is evergreen, this is null or is the cancellation date (if one has been set). .</param>
        /// <param name="termStartDate">Date the subscription term begins. If this is a renewal subscription, this date is different from the subscription start date. .</param>
        /// <param name="termType">Possible values are: &#x60;TERMED&#x60;, &#x60;EVERGREEN&#x60;. .</param>
        /// <param name="totalContractedValue">Total contracted value of the subscription. .</param>
        /// <param name="version">This is the subscription version automatically generated by Zuora Billing. Each order or amendment creates a new version of the subscription, which incorporates the changes made in the order or amendment..</param>
        public GETSubscriptionTypeWithSuccessAllOf(string accountId = default(string), string accountName = default(string), string accountNumber = default(string), bool autoRenew = default(bool), GETAccountSummaryTypeBillToContact billToContact = default(GETAccountSummaryTypeBillToContact), string cancelReason = default(string), DateTime contractEffectiveDate = default(DateTime), decimal contractedMrr = default(decimal), long currentTerm = default(long), string currentTermPeriodType = default(string), DateTime customerAcceptanceDate = default(DateTime), ExternallyManagedByEnum? externallyManagedBy = default(ExternallyManagedByEnum?), string id = default(string), long initialTerm = default(long), string initialTermPeriodType = default(string), string invoiceOwnerAccountId = default(string), string invoiceOwnerAccountName = default(string), string invoiceOwnerAccountNumber = default(string), string invoiceSeparately = default(string), bool isLatestVersion = default(bool), DateTime lastBookingDate = default(DateTime), string notes = default(string), string orderNumber = default(string), string paymentTerm = default(string), List<GETSubscriptionRatePlanType> ratePlans = default(List<GETSubscriptionRatePlanType>), string renewalSetting = default(string), long renewalTerm = default(long), string renewalTermPeriodType = default(string), string revision = default(string), DateTime serviceActivationDate = default(DateTime), string status = default(string), DateTime subscriptionEndDate = default(DateTime), string subscriptionNumber = default(string), DateTime subscriptionStartDate = default(DateTime), bool success = default(bool), DateTime termEndDate = default(DateTime), DateTime termStartDate = default(DateTime), string termType = default(string), decimal totalContractedValue = default(decimal), long version = default(long))
        {
            this.AccountId = accountId;
            this.AccountName = accountName;
            this.AccountNumber = accountNumber;
            this.AutoRenew = autoRenew;
            this.BillToContact = billToContact;
            this.CancelReason = cancelReason;
            this.ContractEffectiveDate = contractEffectiveDate;
            this.ContractedMrr = contractedMrr;
            this.CurrentTerm = currentTerm;
            this.CurrentTermPeriodType = currentTermPeriodType;
            this.CustomerAcceptanceDate = customerAcceptanceDate;
            this.ExternallyManagedBy = externallyManagedBy;
            this.Id = id;
            this.InitialTerm = initialTerm;
            this.InitialTermPeriodType = initialTermPeriodType;
            this.InvoiceOwnerAccountId = invoiceOwnerAccountId;
            this.InvoiceOwnerAccountName = invoiceOwnerAccountName;
            this.InvoiceOwnerAccountNumber = invoiceOwnerAccountNumber;
            this.InvoiceSeparately = invoiceSeparately;
            this.IsLatestVersion = isLatestVersion;
            this.LastBookingDate = lastBookingDate;
            this.Notes = notes;
            this.OrderNumber = orderNumber;
            this.PaymentTerm = paymentTerm;
            this.RatePlans = ratePlans;
            this.RenewalSetting = renewalSetting;
            this.RenewalTerm = renewalTerm;
            this.RenewalTermPeriodType = renewalTermPeriodType;
            this.Revision = revision;
            this.ServiceActivationDate = serviceActivationDate;
            this.Status = status;
            this.SubscriptionEndDate = subscriptionEndDate;
            this.SubscriptionNumber = subscriptionNumber;
            this.SubscriptionStartDate = subscriptionStartDate;
            this.Success = success;
            this.TermEndDate = termEndDate;
            this.TermStartDate = termStartDate;
            this.TermType = termType;
            this.TotalContractedValue = totalContractedValue;
            this._Version = version;
        }

        /// <summary>
        /// The ID of the account associated with this subscription.
        /// </summary>
        /// <value>The ID of the account associated with this subscription.</value>
        [DataMember(Name = "accountId", EmitDefaultValue = false)]
        public string AccountId { get; set; }

        /// <summary>
        /// The name of the account associated with this subscription.
        /// </summary>
        /// <value>The name of the account associated with this subscription.</value>
        [DataMember(Name = "accountName", EmitDefaultValue = false)]
        public string AccountName { get; set; }

        /// <summary>
        /// The number of the account associated with this subscription.
        /// </summary>
        /// <value>The number of the account associated with this subscription.</value>
        [DataMember(Name = "accountNumber", EmitDefaultValue = false)]
        public string AccountNumber { get; set; }

        /// <summary>
        /// If &#x60;true&#x60;, the subscription automatically renews at the end of the term. Default is &#x60;false&#x60;. 
        /// </summary>
        /// <value>If &#x60;true&#x60;, the subscription automatically renews at the end of the term. Default is &#x60;false&#x60;. </value>
        [DataMember(Name = "autoRenew", EmitDefaultValue = true)]
        public bool AutoRenew { get; set; }

        /// <summary>
        /// Gets or Sets BillToContact
        /// </summary>
        [DataMember(Name = "billToContact", EmitDefaultValue = false)]
        public GETAccountSummaryTypeBillToContact BillToContact { get; set; }

        /// <summary>
        /// The reason for a subscription cancellation copied from the &#x60;changeReason&#x60; field of a Cancel Subscription order action.   This field contains valid value only if a subscription is cancelled through the Orders UI or API. Otherwise, the value for this field will always be &#x60;null&#x60;. 
        /// </summary>
        /// <value>The reason for a subscription cancellation copied from the &#x60;changeReason&#x60; field of a Cancel Subscription order action.   This field contains valid value only if a subscription is cancelled through the Orders UI or API. Otherwise, the value for this field will always be &#x60;null&#x60;. </value>
        [DataMember(Name = "cancelReason", EmitDefaultValue = false)]
        public string CancelReason { get; set; }

        /// <summary>
        /// Effective contract date for this subscription, as yyyy-mm-dd. 
        /// </summary>
        /// <value>Effective contract date for this subscription, as yyyy-mm-dd. </value>
        [DataMember(Name = "contractEffectiveDate", EmitDefaultValue = false)]
        [JsonConverter(typeof(OpenAPIDateConverter))]
        public DateTime ContractEffectiveDate { get; set; }

        /// <summary>
        /// Monthly recurring revenue of the subscription. 
        /// </summary>
        /// <value>Monthly recurring revenue of the subscription. </value>
        [DataMember(Name = "contractedMrr", EmitDefaultValue = false)]
        public decimal ContractedMrr { get; set; }

        /// <summary>
        /// The length of the period for the current subscription term. 
        /// </summary>
        /// <value>The length of the period for the current subscription term. </value>
        [DataMember(Name = "currentTerm", EmitDefaultValue = false)]
        public long CurrentTerm { get; set; }

        /// <summary>
        /// The period type for the current subscription term.  Values are:  * &#x60;Month&#x60; (default) * &#x60;Year&#x60; * &#x60;Day&#x60; * &#x60;Week&#x60; 
        /// </summary>
        /// <value>The period type for the current subscription term.  Values are:  * &#x60;Month&#x60; (default) * &#x60;Year&#x60; * &#x60;Day&#x60; * &#x60;Week&#x60; </value>
        [DataMember(Name = "currentTermPeriodType", EmitDefaultValue = false)]
        public string CurrentTermPeriodType { get; set; }

        /// <summary>
        /// The date on which the services or products within a subscription have been accepted by the customer, as yyyy-mm-dd. 
        /// </summary>
        /// <value>The date on which the services or products within a subscription have been accepted by the customer, as yyyy-mm-dd. </value>
        [DataMember(Name = "customerAcceptanceDate", EmitDefaultValue = false)]
        [JsonConverter(typeof(OpenAPIDateConverter))]
        public DateTime CustomerAcceptanceDate { get; set; }

        /// <summary>
        /// Subscription ID. 
        /// </summary>
        /// <value>Subscription ID. </value>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }

        /// <summary>
        /// The length of the period for the first subscription term. 
        /// </summary>
        /// <value>The length of the period for the first subscription term. </value>
        [DataMember(Name = "initialTerm", EmitDefaultValue = false)]
        public long InitialTerm { get; set; }

        /// <summary>
        /// The period type for the first subscription term.  Values are:  * &#x60;Month&#x60; (default) * &#x60;Year&#x60; * &#x60;Day&#x60; * &#x60;Week&#x60; 
        /// </summary>
        /// <value>The period type for the first subscription term.  Values are:  * &#x60;Month&#x60; (default) * &#x60;Year&#x60; * &#x60;Day&#x60; * &#x60;Week&#x60; </value>
        [DataMember(Name = "initialTermPeriodType", EmitDefaultValue = false)]
        public string InitialTermPeriodType { get; set; }

        /// <summary>
        /// Gets or Sets InvoiceOwnerAccountId
        /// </summary>
        [DataMember(Name = "invoiceOwnerAccountId", EmitDefaultValue = false)]
        public string InvoiceOwnerAccountId { get; set; }

        /// <summary>
        /// Gets or Sets InvoiceOwnerAccountName
        /// </summary>
        [DataMember(Name = "invoiceOwnerAccountName", EmitDefaultValue = false)]
        public string InvoiceOwnerAccountName { get; set; }

        /// <summary>
        /// Gets or Sets InvoiceOwnerAccountNumber
        /// </summary>
        [DataMember(Name = "invoiceOwnerAccountNumber", EmitDefaultValue = false)]
        public string InvoiceOwnerAccountNumber { get; set; }

        /// <summary>
        /// Separates a single subscription from other subscriptions and creates an invoice for the subscription.   If the value is &#x60;true&#x60;, the subscription is billed separately from other subscriptions. If the value is &#x60;false&#x60;, the subscription is included with other subscriptions in the account invoice. 
        /// </summary>
        /// <value>Separates a single subscription from other subscriptions and creates an invoice for the subscription.   If the value is &#x60;true&#x60;, the subscription is billed separately from other subscriptions. If the value is &#x60;false&#x60;, the subscription is included with other subscriptions in the account invoice. </value>
        [DataMember(Name = "invoiceSeparately", EmitDefaultValue = false)]
        public string InvoiceSeparately { get; set; }

        /// <summary>
        /// If &#x60;true&#x60;, the current subscription object is the latest version.
        /// </summary>
        /// <value>If &#x60;true&#x60;, the current subscription object is the latest version.</value>
        [DataMember(Name = "isLatestVersion", EmitDefaultValue = true)]
        public bool IsLatestVersion { get; set; }

        /// <summary>
        /// The last booking date of the subscription object. This field is writable only when the subscription is newly created as a first version subscription. You can override the date value when creating a subscription through the Subscribe and Amend API or the subscription creation UI (non-Orders). Otherwise, the default value &#x60;today&#x60; is set per the user&#39;s timezone. The value of this field is as follows: * For a new subscription created by the [Subscribe and Amend APIs](https://knowledgecenter.zuora.com/Billing/Subscriptions/Orders/Orders_Harmonization/Orders_Migration_Guidance#Subscribe_and_Amend_APIs_to_Migrate), this field has the value of the subscription creation date. * For a subscription changed by an amendment, this field has the value of the amendment booking date. * For a subscription created or changed by an order, this field has the value of the order date. 
        /// </summary>
        /// <value>The last booking date of the subscription object. This field is writable only when the subscription is newly created as a first version subscription. You can override the date value when creating a subscription through the Subscribe and Amend API or the subscription creation UI (non-Orders). Otherwise, the default value &#x60;today&#x60; is set per the user&#39;s timezone. The value of this field is as follows: * For a new subscription created by the [Subscribe and Amend APIs](https://knowledgecenter.zuora.com/Billing/Subscriptions/Orders/Orders_Harmonization/Orders_Migration_Guidance#Subscribe_and_Amend_APIs_to_Migrate), this field has the value of the subscription creation date. * For a subscription changed by an amendment, this field has the value of the amendment booking date. * For a subscription created or changed by an order, this field has the value of the order date. </value>
        [DataMember(Name = "lastBookingDate", EmitDefaultValue = false)]
        [JsonConverter(typeof(OpenAPIDateConverter))]
        public DateTime LastBookingDate { get; set; }

        /// <summary>
        /// A string of up to 65,535 characters. 
        /// </summary>
        /// <value>A string of up to 65,535 characters. </value>
        [DataMember(Name = "notes", EmitDefaultValue = false)]
        public string Notes { get; set; }

        /// <summary>
        /// The order number of the order in which the changes on the subscription are made.   **Note:** This field is only available if you have the [Order Metrics](https://knowledgecenter.zuora.com/BC_Subscription_Management/Orders/AA_Overview_of_Orders#Order_Metrics) feature enabled. If you wish to have access to the feature, submit a request at [Zuora Global Support](http://support.zuora.com/). We will investigate your use cases and data before enabling this feature for you. 
        /// </summary>
        /// <value>The order number of the order in which the changes on the subscription are made.   **Note:** This field is only available if you have the [Order Metrics](https://knowledgecenter.zuora.com/BC_Subscription_Management/Orders/AA_Overview_of_Orders#Order_Metrics) feature enabled. If you wish to have access to the feature, submit a request at [Zuora Global Support](http://support.zuora.com/). We will investigate your use cases and data before enabling this feature for you. </value>
        [DataMember(Name = "orderNumber", EmitDefaultValue = false)]
        public string OrderNumber { get; set; }

        /// <summary>
        /// The name of the payment term associated with the subscription. For example, &#x60;Net 30&#x60;. The payment term determines the due dates of invoices.  **Note**: The value of this field is &#x60;null&#x60; if you have the [Flexible Billing](https://knowledgecenter.zuora.com/Billing/Subscriptions/Flexible_Billing) feature disabled. 
        /// </summary>
        /// <value>The name of the payment term associated with the subscription. For example, &#x60;Net 30&#x60;. The payment term determines the due dates of invoices.  **Note**: The value of this field is &#x60;null&#x60; if you have the [Flexible Billing](https://knowledgecenter.zuora.com/Billing/Subscriptions/Flexible_Billing) feature disabled. </value>
        [DataMember(Name = "paymentTerm", EmitDefaultValue = false)]
        public string PaymentTerm { get; set; }

        /// <summary>
        /// Container for rate plans. 
        /// </summary>
        /// <value>Container for rate plans. </value>
        [DataMember(Name = "ratePlans", EmitDefaultValue = false)]
        public List<GETSubscriptionRatePlanType> RatePlans { get; set; }

        /// <summary>
        /// Specifies whether a termed subscription will remain &#x60;TERMED&#x60; or change to &#x60;EVERGREEN&#x60; when it is renewed.   Values are:  * &#x60;RENEW_WITH_SPECIFIC_TERM&#x60; (default) * &#x60;RENEW_TO_EVERGREEN&#x60; 
        /// </summary>
        /// <value>Specifies whether a termed subscription will remain &#x60;TERMED&#x60; or change to &#x60;EVERGREEN&#x60; when it is renewed.   Values are:  * &#x60;RENEW_WITH_SPECIFIC_TERM&#x60; (default) * &#x60;RENEW_TO_EVERGREEN&#x60; </value>
        [DataMember(Name = "renewalSetting", EmitDefaultValue = false)]
        public string RenewalSetting { get; set; }

        /// <summary>
        /// The length of the period for the subscription renewal term. 
        /// </summary>
        /// <value>The length of the period for the subscription renewal term. </value>
        [DataMember(Name = "renewalTerm", EmitDefaultValue = false)]
        public long RenewalTerm { get; set; }

        /// <summary>
        /// The period type for the subscription renewal term.  Values are:  * &#x60;Month&#x60; (default) * &#x60;Year&#x60; * &#x60;Day&#x60; * &#x60;Week&#x60; 
        /// </summary>
        /// <value>The period type for the subscription renewal term.  Values are:  * &#x60;Month&#x60; (default) * &#x60;Year&#x60; * &#x60;Day&#x60; * &#x60;Week&#x60; </value>
        [DataMember(Name = "renewalTermPeriodType", EmitDefaultValue = false)]
        public string RenewalTermPeriodType { get; set; }

        /// <summary>
        /// An auto-generated decimal value uniquely tagged with a subscription. The value always contains one decimal place, for example, the revision of a new subscription is 1.0. If a further version of the subscription is created, the revision value will be increased by 1. Also, the revision value is always incremental regardless of deletion of subscription versions. 
        /// </summary>
        /// <value>An auto-generated decimal value uniquely tagged with a subscription. The value always contains one decimal place, for example, the revision of a new subscription is 1.0. If a further version of the subscription is created, the revision value will be increased by 1. Also, the revision value is always incremental regardless of deletion of subscription versions. </value>
        [DataMember(Name = "revision", EmitDefaultValue = false)]
        public string Revision { get; set; }

        /// <summary>
        /// The date on which the services or products within a subscription have been activated and access has been provided to the customer, as yyyy-mm-dd 
        /// </summary>
        /// <value>The date on which the services or products within a subscription have been activated and access has been provided to the customer, as yyyy-mm-dd </value>
        [DataMember(Name = "serviceActivationDate", EmitDefaultValue = false)]
        [JsonConverter(typeof(OpenAPIDateConverter))]
        public DateTime ServiceActivationDate { get; set; }

        /// <summary>
        /// Subscription status; possible values are:  * &#x60;Draft&#x60; * &#x60;Pending Activation&#x60; * &#x60;Pending Acceptance&#x60; * &#x60;Active&#x60; * &#x60;Cancelled&#x60; * &#x60;Suspended&#x60; 
        /// </summary>
        /// <value>Subscription status; possible values are:  * &#x60;Draft&#x60; * &#x60;Pending Activation&#x60; * &#x60;Pending Acceptance&#x60; * &#x60;Active&#x60; * &#x60;Cancelled&#x60; * &#x60;Suspended&#x60; </value>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public string Status { get; set; }

        /// <summary>
        /// The date when the subscription term ends, where the subscription ends at midnight the day before. For example, if the &#x60;subscriptionEndDate&#x60; is 12/31/2016, the subscriptions ends at midnight (00:00:00 hours) on 12/30/2016. This date is the same as the term end date or the cancelation date, as appropriate. 
        /// </summary>
        /// <value>The date when the subscription term ends, where the subscription ends at midnight the day before. For example, if the &#x60;subscriptionEndDate&#x60; is 12/31/2016, the subscriptions ends at midnight (00:00:00 hours) on 12/30/2016. This date is the same as the term end date or the cancelation date, as appropriate. </value>
        [DataMember(Name = "subscriptionEndDate", EmitDefaultValue = false)]
        [JsonConverter(typeof(OpenAPIDateConverter))]
        public DateTime SubscriptionEndDate { get; set; }

        /// <summary>
        /// Subscription number.
        /// </summary>
        /// <value>Subscription number.</value>
        [DataMember(Name = "subscriptionNumber", EmitDefaultValue = false)]
        public string SubscriptionNumber { get; set; }

        /// <summary>
        /// Date the subscription becomes effective. 
        /// </summary>
        /// <value>Date the subscription becomes effective. </value>
        [DataMember(Name = "subscriptionStartDate", EmitDefaultValue = false)]
        [JsonConverter(typeof(OpenAPIDateConverter))]
        public DateTime SubscriptionStartDate { get; set; }

        /// <summary>
        /// Returns &#x60;true&#x60; if the request was processed successfully. 
        /// </summary>
        /// <value>Returns &#x60;true&#x60; if the request was processed successfully. </value>
        [DataMember(Name = "success", EmitDefaultValue = true)]
        public bool Success { get; set; }

        /// <summary>
        /// Date the subscription term ends. If the subscription is evergreen, this is null or is the cancellation date (if one has been set). 
        /// </summary>
        /// <value>Date the subscription term ends. If the subscription is evergreen, this is null or is the cancellation date (if one has been set). </value>
        [DataMember(Name = "termEndDate", EmitDefaultValue = false)]
        [JsonConverter(typeof(OpenAPIDateConverter))]
        public DateTime TermEndDate { get; set; }

        /// <summary>
        /// Date the subscription term begins. If this is a renewal subscription, this date is different from the subscription start date. 
        /// </summary>
        /// <value>Date the subscription term begins. If this is a renewal subscription, this date is different from the subscription start date. </value>
        [DataMember(Name = "termStartDate", EmitDefaultValue = false)]
        [JsonConverter(typeof(OpenAPIDateConverter))]
        public DateTime TermStartDate { get; set; }

        /// <summary>
        /// Possible values are: &#x60;TERMED&#x60;, &#x60;EVERGREEN&#x60;. 
        /// </summary>
        /// <value>Possible values are: &#x60;TERMED&#x60;, &#x60;EVERGREEN&#x60;. </value>
        [DataMember(Name = "termType", EmitDefaultValue = false)]
        public string TermType { get; set; }

        /// <summary>
        /// Total contracted value of the subscription. 
        /// </summary>
        /// <value>Total contracted value of the subscription. </value>
        [DataMember(Name = "totalContractedValue", EmitDefaultValue = false)]
        public decimal TotalContractedValue { get; set; }

        /// <summary>
        /// This is the subscription version automatically generated by Zuora Billing. Each order or amendment creates a new version of the subscription, which incorporates the changes made in the order or amendment.
        /// </summary>
        /// <value>This is the subscription version automatically generated by Zuora Billing. Each order or amendment creates a new version of the subscription, which incorporates the changes made in the order or amendment.</value>
        [DataMember(Name = "version", EmitDefaultValue = false)]
        public long _Version { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class GETSubscriptionTypeWithSuccessAllOf {\n");
            sb.Append("  AccountId: ").Append(AccountId).Append("\n");
            sb.Append("  AccountName: ").Append(AccountName).Append("\n");
            sb.Append("  AccountNumber: ").Append(AccountNumber).Append("\n");
            sb.Append("  AutoRenew: ").Append(AutoRenew).Append("\n");
            sb.Append("  BillToContact: ").Append(BillToContact).Append("\n");
            sb.Append("  CancelReason: ").Append(CancelReason).Append("\n");
            sb.Append("  ContractEffectiveDate: ").Append(ContractEffectiveDate).Append("\n");
            sb.Append("  ContractedMrr: ").Append(ContractedMrr).Append("\n");
            sb.Append("  CurrentTerm: ").Append(CurrentTerm).Append("\n");
            sb.Append("  CurrentTermPeriodType: ").Append(CurrentTermPeriodType).Append("\n");
            sb.Append("  CustomerAcceptanceDate: ").Append(CustomerAcceptanceDate).Append("\n");
            sb.Append("  ExternallyManagedBy: ").Append(ExternallyManagedBy).Append("\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  InitialTerm: ").Append(InitialTerm).Append("\n");
            sb.Append("  InitialTermPeriodType: ").Append(InitialTermPeriodType).Append("\n");
            sb.Append("  InvoiceOwnerAccountId: ").Append(InvoiceOwnerAccountId).Append("\n");
            sb.Append("  InvoiceOwnerAccountName: ").Append(InvoiceOwnerAccountName).Append("\n");
            sb.Append("  InvoiceOwnerAccountNumber: ").Append(InvoiceOwnerAccountNumber).Append("\n");
            sb.Append("  InvoiceSeparately: ").Append(InvoiceSeparately).Append("\n");
            sb.Append("  IsLatestVersion: ").Append(IsLatestVersion).Append("\n");
            sb.Append("  LastBookingDate: ").Append(LastBookingDate).Append("\n");
            sb.Append("  Notes: ").Append(Notes).Append("\n");
            sb.Append("  OrderNumber: ").Append(OrderNumber).Append("\n");
            sb.Append("  PaymentTerm: ").Append(PaymentTerm).Append("\n");
            sb.Append("  RatePlans: ").Append(RatePlans).Append("\n");
            sb.Append("  RenewalSetting: ").Append(RenewalSetting).Append("\n");
            sb.Append("  RenewalTerm: ").Append(RenewalTerm).Append("\n");
            sb.Append("  RenewalTermPeriodType: ").Append(RenewalTermPeriodType).Append("\n");
            sb.Append("  Revision: ").Append(Revision).Append("\n");
            sb.Append("  ServiceActivationDate: ").Append(ServiceActivationDate).Append("\n");
            sb.Append("  Status: ").Append(Status).Append("\n");
            sb.Append("  SubscriptionEndDate: ").Append(SubscriptionEndDate).Append("\n");
            sb.Append("  SubscriptionNumber: ").Append(SubscriptionNumber).Append("\n");
            sb.Append("  SubscriptionStartDate: ").Append(SubscriptionStartDate).Append("\n");
            sb.Append("  Success: ").Append(Success).Append("\n");
            sb.Append("  TermEndDate: ").Append(TermEndDate).Append("\n");
            sb.Append("  TermStartDate: ").Append(TermStartDate).Append("\n");
            sb.Append("  TermType: ").Append(TermType).Append("\n");
            sb.Append("  TotalContractedValue: ").Append(TotalContractedValue).Append("\n");
            sb.Append("  _Version: ").Append(_Version).Append("\n");
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
            return this.Equals(input as GETSubscriptionTypeWithSuccessAllOf);
        }

        /// <summary>
        /// Returns true if GETSubscriptionTypeWithSuccessAllOf instances are equal
        /// </summary>
        /// <param name="input">Instance of GETSubscriptionTypeWithSuccessAllOf to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(GETSubscriptionTypeWithSuccessAllOf input)
        {
            if (input == null)
            {
                return false;
            }
            return 
                (
                    this.AccountId == input.AccountId ||
                    (this.AccountId != null &&
                    this.AccountId.Equals(input.AccountId))
                ) && 
                (
                    this.AccountName == input.AccountName ||
                    (this.AccountName != null &&
                    this.AccountName.Equals(input.AccountName))
                ) && 
                (
                    this.AccountNumber == input.AccountNumber ||
                    (this.AccountNumber != null &&
                    this.AccountNumber.Equals(input.AccountNumber))
                ) && 
                (
                    this.AutoRenew == input.AutoRenew ||
                    this.AutoRenew.Equals(input.AutoRenew)
                ) && 
                (
                    this.BillToContact == input.BillToContact ||
                    (this.BillToContact != null &&
                    this.BillToContact.Equals(input.BillToContact))
                ) && 
                (
                    this.CancelReason == input.CancelReason ||
                    (this.CancelReason != null &&
                    this.CancelReason.Equals(input.CancelReason))
                ) && 
                (
                    this.ContractEffectiveDate == input.ContractEffectiveDate ||
                    (this.ContractEffectiveDate != null &&
                    this.ContractEffectiveDate.Equals(input.ContractEffectiveDate))
                ) && 
                (
                    this.ContractedMrr == input.ContractedMrr ||
                    this.ContractedMrr.Equals(input.ContractedMrr)
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
                    this.ExternallyManagedBy == input.ExternallyManagedBy ||
                    this.ExternallyManagedBy.Equals(input.ExternallyManagedBy)
                ) && 
                (
                    this.Id == input.Id ||
                    (this.Id != null &&
                    this.Id.Equals(input.Id))
                ) && 
                (
                    this.InitialTerm == input.InitialTerm ||
                    this.InitialTerm.Equals(input.InitialTerm)
                ) && 
                (
                    this.InitialTermPeriodType == input.InitialTermPeriodType ||
                    (this.InitialTermPeriodType != null &&
                    this.InitialTermPeriodType.Equals(input.InitialTermPeriodType))
                ) && 
                (
                    this.InvoiceOwnerAccountId == input.InvoiceOwnerAccountId ||
                    (this.InvoiceOwnerAccountId != null &&
                    this.InvoiceOwnerAccountId.Equals(input.InvoiceOwnerAccountId))
                ) && 
                (
                    this.InvoiceOwnerAccountName == input.InvoiceOwnerAccountName ||
                    (this.InvoiceOwnerAccountName != null &&
                    this.InvoiceOwnerAccountName.Equals(input.InvoiceOwnerAccountName))
                ) && 
                (
                    this.InvoiceOwnerAccountNumber == input.InvoiceOwnerAccountNumber ||
                    (this.InvoiceOwnerAccountNumber != null &&
                    this.InvoiceOwnerAccountNumber.Equals(input.InvoiceOwnerAccountNumber))
                ) && 
                (
                    this.InvoiceSeparately == input.InvoiceSeparately ||
                    (this.InvoiceSeparately != null &&
                    this.InvoiceSeparately.Equals(input.InvoiceSeparately))
                ) && 
                (
                    this.IsLatestVersion == input.IsLatestVersion ||
                    this.IsLatestVersion.Equals(input.IsLatestVersion)
                ) && 
                (
                    this.LastBookingDate == input.LastBookingDate ||
                    (this.LastBookingDate != null &&
                    this.LastBookingDate.Equals(input.LastBookingDate))
                ) && 
                (
                    this.Notes == input.Notes ||
                    (this.Notes != null &&
                    this.Notes.Equals(input.Notes))
                ) && 
                (
                    this.OrderNumber == input.OrderNumber ||
                    (this.OrderNumber != null &&
                    this.OrderNumber.Equals(input.OrderNumber))
                ) && 
                (
                    this.PaymentTerm == input.PaymentTerm ||
                    (this.PaymentTerm != null &&
                    this.PaymentTerm.Equals(input.PaymentTerm))
                ) && 
                (
                    this.RatePlans == input.RatePlans ||
                    this.RatePlans != null &&
                    input.RatePlans != null &&
                    this.RatePlans.SequenceEqual(input.RatePlans)
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
                    this.Revision == input.Revision ||
                    (this.Revision != null &&
                    this.Revision.Equals(input.Revision))
                ) && 
                (
                    this.ServiceActivationDate == input.ServiceActivationDate ||
                    (this.ServiceActivationDate != null &&
                    this.ServiceActivationDate.Equals(input.ServiceActivationDate))
                ) && 
                (
                    this.Status == input.Status ||
                    (this.Status != null &&
                    this.Status.Equals(input.Status))
                ) && 
                (
                    this.SubscriptionEndDate == input.SubscriptionEndDate ||
                    (this.SubscriptionEndDate != null &&
                    this.SubscriptionEndDate.Equals(input.SubscriptionEndDate))
                ) && 
                (
                    this.SubscriptionNumber == input.SubscriptionNumber ||
                    (this.SubscriptionNumber != null &&
                    this.SubscriptionNumber.Equals(input.SubscriptionNumber))
                ) && 
                (
                    this.SubscriptionStartDate == input.SubscriptionStartDate ||
                    (this.SubscriptionStartDate != null &&
                    this.SubscriptionStartDate.Equals(input.SubscriptionStartDate))
                ) && 
                (
                    this.Success == input.Success ||
                    this.Success.Equals(input.Success)
                ) && 
                (
                    this.TermEndDate == input.TermEndDate ||
                    (this.TermEndDate != null &&
                    this.TermEndDate.Equals(input.TermEndDate))
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
                    this.TotalContractedValue == input.TotalContractedValue ||
                    this.TotalContractedValue.Equals(input.TotalContractedValue)
                ) && 
                (
                    this._Version == input._Version ||
                    this._Version.Equals(input._Version)
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
                if (this.AccountId != null)
                {
                    hashCode = (hashCode * 59) + this.AccountId.GetHashCode();
                }
                if (this.AccountName != null)
                {
                    hashCode = (hashCode * 59) + this.AccountName.GetHashCode();
                }
                if (this.AccountNumber != null)
                {
                    hashCode = (hashCode * 59) + this.AccountNumber.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.AutoRenew.GetHashCode();
                if (this.BillToContact != null)
                {
                    hashCode = (hashCode * 59) + this.BillToContact.GetHashCode();
                }
                if (this.CancelReason != null)
                {
                    hashCode = (hashCode * 59) + this.CancelReason.GetHashCode();
                }
                if (this.ContractEffectiveDate != null)
                {
                    hashCode = (hashCode * 59) + this.ContractEffectiveDate.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.ContractedMrr.GetHashCode();
                hashCode = (hashCode * 59) + this.CurrentTerm.GetHashCode();
                if (this.CurrentTermPeriodType != null)
                {
                    hashCode = (hashCode * 59) + this.CurrentTermPeriodType.GetHashCode();
                }
                if (this.CustomerAcceptanceDate != null)
                {
                    hashCode = (hashCode * 59) + this.CustomerAcceptanceDate.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.ExternallyManagedBy.GetHashCode();
                if (this.Id != null)
                {
                    hashCode = (hashCode * 59) + this.Id.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.InitialTerm.GetHashCode();
                if (this.InitialTermPeriodType != null)
                {
                    hashCode = (hashCode * 59) + this.InitialTermPeriodType.GetHashCode();
                }
                if (this.InvoiceOwnerAccountId != null)
                {
                    hashCode = (hashCode * 59) + this.InvoiceOwnerAccountId.GetHashCode();
                }
                if (this.InvoiceOwnerAccountName != null)
                {
                    hashCode = (hashCode * 59) + this.InvoiceOwnerAccountName.GetHashCode();
                }
                if (this.InvoiceOwnerAccountNumber != null)
                {
                    hashCode = (hashCode * 59) + this.InvoiceOwnerAccountNumber.GetHashCode();
                }
                if (this.InvoiceSeparately != null)
                {
                    hashCode = (hashCode * 59) + this.InvoiceSeparately.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.IsLatestVersion.GetHashCode();
                if (this.LastBookingDate != null)
                {
                    hashCode = (hashCode * 59) + this.LastBookingDate.GetHashCode();
                }
                if (this.Notes != null)
                {
                    hashCode = (hashCode * 59) + this.Notes.GetHashCode();
                }
                if (this.OrderNumber != null)
                {
                    hashCode = (hashCode * 59) + this.OrderNumber.GetHashCode();
                }
                if (this.PaymentTerm != null)
                {
                    hashCode = (hashCode * 59) + this.PaymentTerm.GetHashCode();
                }
                if (this.RatePlans != null)
                {
                    hashCode = (hashCode * 59) + this.RatePlans.GetHashCode();
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
                if (this.Revision != null)
                {
                    hashCode = (hashCode * 59) + this.Revision.GetHashCode();
                }
                if (this.ServiceActivationDate != null)
                {
                    hashCode = (hashCode * 59) + this.ServiceActivationDate.GetHashCode();
                }
                if (this.Status != null)
                {
                    hashCode = (hashCode * 59) + this.Status.GetHashCode();
                }
                if (this.SubscriptionEndDate != null)
                {
                    hashCode = (hashCode * 59) + this.SubscriptionEndDate.GetHashCode();
                }
                if (this.SubscriptionNumber != null)
                {
                    hashCode = (hashCode * 59) + this.SubscriptionNumber.GetHashCode();
                }
                if (this.SubscriptionStartDate != null)
                {
                    hashCode = (hashCode * 59) + this.SubscriptionStartDate.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.Success.GetHashCode();
                if (this.TermEndDate != null)
                {
                    hashCode = (hashCode * 59) + this.TermEndDate.GetHashCode();
                }
                if (this.TermStartDate != null)
                {
                    hashCode = (hashCode * 59) + this.TermStartDate.GetHashCode();
                }
                if (this.TermType != null)
                {
                    hashCode = (hashCode * 59) + this.TermType.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.TotalContractedValue.GetHashCode();
                hashCode = (hashCode * 59) + this._Version.GetHashCode();
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
