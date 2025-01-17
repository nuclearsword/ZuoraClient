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
    ///  This is the Account object containing the information for this particular subscription. It has all the information needed to create an account for a subscription.  **Values:** A valid account.
    /// </summary>
    [DataContract(Name = "SubscribeRequestAccount")]
    public partial class SubscribeRequestAccount : IEquatable<SubscribeRequestAccount>, IValidatableObject
    {
        /// <summary>
        /// Value of the Customer Type field for the corresponding customer account in NetSuite. The Customer Type field is used when the customer account is created in NetSuite. Only available if you have installed the [Zuora Connector for NetSuite](https://www.zuora.com/connect/app/?appId&#x3D;265). 
        /// </summary>
        /// <value>Value of the Customer Type field for the corresponding customer account in NetSuite. The Customer Type field is used when the customer account is created in NetSuite. Only available if you have installed the [Zuora Connector for NetSuite](https://www.zuora.com/connect/app/?appId&#x3D;265). </value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum CustomerTypeNSEnum
        {
            /// <summary>
            /// Enum Company for value: Company
            /// </summary>
            [EnumMember(Value = "Company")]
            Company = 1,

            /// <summary>
            /// Enum Individual for value: Individual
            /// </summary>
            [EnumMember(Value = "Individual")]
            Individual = 2

        }


        /// <summary>
        /// Value of the Customer Type field for the corresponding customer account in NetSuite. The Customer Type field is used when the customer account is created in NetSuite. Only available if you have installed the [Zuora Connector for NetSuite](https://www.zuora.com/connect/app/?appId&#x3D;265). 
        /// </summary>
        /// <value>Value of the Customer Type field for the corresponding customer account in NetSuite. The Customer Type field is used when the customer account is created in NetSuite. Only available if you have installed the [Zuora Connector for NetSuite](https://www.zuora.com/connect/app/?appId&#x3D;265). </value>
        [DataMember(Name = "CustomerType__NS", EmitDefaultValue = false)]
        public CustomerTypeNSEnum? CustomerTypeNS { get; set; }
        /// <summary>
        /// Specifies whether the account should be synchronized with NetSuite. Only available if you have installed the [Zuora Connector for NetSuite](https://www.zuora.com/connect/app/?appId&#x3D;265). 
        /// </summary>
        /// <value>Specifies whether the account should be synchronized with NetSuite. Only available if you have installed the [Zuora Connector for NetSuite](https://www.zuora.com/connect/app/?appId&#x3D;265). </value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum SynctoNetSuiteNSEnum
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
        /// Specifies whether the account should be synchronized with NetSuite. Only available if you have installed the [Zuora Connector for NetSuite](https://www.zuora.com/connect/app/?appId&#x3D;265). 
        /// </summary>
        /// <value>Specifies whether the account should be synchronized with NetSuite. Only available if you have installed the [Zuora Connector for NetSuite](https://www.zuora.com/connect/app/?appId&#x3D;265). </value>
        [DataMember(Name = "SynctoNetSuite__NS", EmitDefaultValue = false)]
        public SynctoNetSuiteNSEnum? SynctoNetSuiteNS { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="SubscribeRequestAccount" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected SubscribeRequestAccount() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="SubscribeRequestAccount" /> class.
        /// </summary>
        /// <param name="accountNumber">Unique account number assigned to the account.  **Character limit**: 50   **Values**: one of the following:  - null to auto-generate - a string of 50 characters or fewer that doesn&#39;t begin with the default account number prefix .</param>
        /// <param name="additionalEmailAddresses">List of additional email addresses to receive emailed invoices.  **Character limit**: 120   **Values**: comma-separated list of email addresses .</param>
        /// <param name="allowInvoiceEdit"> Indicates if associated invoices can be edited.   **Character limit**: 5   **Values**: &#x60;true&#x60;, &#x60;false&#x60; (default if left null) .</param>
        /// <param name="autoPay"> Indicates if future payments are automatically collected when they&#39;re due during a Payment Run.   **Character limit**: 5   **Values**: &#x60;true&#x60;, &#x60;false&#x60; (default) .</param>
        /// <param name="batch"> Organizes your customer accounts into groups to optimize your billing and payment operations.   **Character limit**: 20   **Values**:any system-defined batch (&#x60;Batch1&#x60; - &#x60;Batch50 &#x60;or by name).  (required).</param>
        /// <param name="bcdSettingOption">Billing cycle day setting option.  **Character limit**: 9   **Values**: &#x60;AutoSet&#x60;, &#x60;ManualSet&#x60; .</param>
        /// <param name="billCycleDay">Billing cycle day (BCD) on which bill runs generate invoices for the account.  **Character limit**: 2   **Values**: any activated system-defined bill cycle day (&#x60;1&#x60; - &#x60;31&#x60;)  (required).</param>
        /// <param name="communicationProfileId">Associates the account with a specified communication profile.  **Character limit**: 32   **Values**: a valid communication profile ID .</param>
        /// <param name="crmId">CRM account ID for the account. A CRM is a customer relationship management system, such as Salesforce.com.  **Character limit**: 100   **Values**: a string of 100 characters or fewer .</param>
        /// <param name="currency"> Currency that the customer is billed in. See [a currency value defined in the Zuora Ui admin settings](https://knowledgecenter.zuora.com/CB_Billing/Billing_Settings/Customize_Currencies)  (required).</param>
        /// <param name="customerServiceRepName">Name of the account&#39;s customer service representative, if applicable.  **Character limit**: 50   **Values**: a string of 50 characters or fewer .</param>
        /// <param name="defaultPaymentMethodId">ID of the default payment method for the account. This field is only required if the &#x60;AutoPay&#x60; field is set to &#x60;true&#x60;. For a specified credit card payment method, it is recommended that [the support for stored credential transactions](https://knowledgecenter.zuora.com/Billing/Billing_and_Payments/L_Payment_Methods/Stored_credential_transactions) for this payment method is already enabled. **Character limit**: 32   **Values**: A valid ID for an existing payment method. This field does not support external payment methods. .</param>
        /// <param name="id">Internal identifier of an existing account. Only set this field if you want to assign the subscription to an existing account..</param>
        /// <param name="invoiceDeliveryPrefsEmail">Indicates if the customer wants to receive invoices through email. **Character limit**: 5   **Values**: &#x60;true&#x60;, &#x60;false&#x60; (default if left null) .</param>
        /// <param name="invoiceDeliveryPrefsPrint">Indicates if the customer wants to receive printed invoices, such as through postal mail.  **Character limit**: 5   **Values**: &#x60;true&#x60;, &#x60;false&#x60; (default if left null) .</param>
        /// <param name="invoiceTemplateId">The ID of the invoice template. Each customer account can use a specific invoice template for invoice generation.  **Character limit**: 32   **Values**: a[ valid template ID configured in Z-Billing Settings](https://knowledgecenter.zuora.com/CB_Billing/Billing_Settings/Manage_Invoice_Rules_and_Templates) To find the ID of your current invoice template: In Zuora, navigate to **Settings &gt; Z-Billing &gt; Manage Invoice Rules and Templates** and click **Show Id **next to the template you want to use.   .</param>
        /// <param name="lastInvoiceDate"> The date when the previous invoice was generated for the account. The field value is null if no invoice has ever been generated for the account.   **Character limit**: 29   **Values**: automatically generated .</param>
        /// <param name="name">Name of the account as displayed in the Zuora UI.  **Character limit**: 255   **Values**: a string of 255 characters or fewer  (required).</param>
        /// <param name="notes"> Comments about the account.  **Character limit**: 65,535   **Values**: a string of 65,535 characters .</param>
        /// <param name="parentId">Identifier of the parent customer account for this Account object. Use this field if you have customer hierarchy enabled.  **Character limit**: 32   **Values**: a valid account ID .</param>
        /// <param name="paymentGateway">Gateway used for processing electronic payments and refunds. This field is only required if there is no default payment gateway is defined in the tenant.  **Character limit**: 40   **Values**: one of the following:  - a valid configured gateway name - Null to inherit the default value set in Z-Payment Settings .</param>
        /// <param name="paymentTerm">Indicates when the customer pays for subscriptions.  **Character limit**: 100   **Values**: [a valid, active payment term defined in the web-based UI administrative settings](https://knowledgecenter.zuora.com/CB_Billing/Billing_Settings/Define_Payment_Terms) .</param>
        /// <param name="purchaseOrderNumber">The number of the purchase order associated with this account. Purchase order information generally comes from customers.  **Character limit**: 100   **Values**: a string of 100 characters or fewer .</param>
        /// <param name="salesRepName">The name of the sales representative associated with this account, if applicable.  **Character limit**: 50   **Values**: a string of 50 characters or fewer .</param>
        /// <param name="taxCompanyCode"> Unique code that identifies a company account in Avalara. Use this field to calculate taxes based on origin and sold-to addresses in Avalara. This feature is in **Limited Availability**. If you wish to have access to the feature, submit a request at [Zuora Global Support](http://support.zuora.com/).    **Character limit**: 50   **Values**: a valid company code .</param>
        /// <param name="taxExemptCertificateID">ID of your customer&#39;s tax exemption certificate.  **Character limit**: 32   **Values**: a string of 32 characters or fewer .</param>
        /// <param name="taxExemptCertificateType">Type of the tax exemption certificate that your customer holds. **Character limit**: 32   **Values**: a string of 32 characters or fewer .</param>
        /// <param name="taxExemptDescription">Description of the tax exemption certificate that your customer holds.  **Character limit**: 500   **Values**: a string of 500 characters or fewer .</param>
        /// <param name="taxExemptEffectiveDate">Date when the the customer&#39;s tax exemption starts.  **Character limit**: 29 **Version notes**: requires Z-Tax .</param>
        /// <param name="taxExemptExpirationDate">Date when the customer&#39;s tax exemption certificate expires **Character limit**: 29 **Version notes**: requires Z-Tax .</param>
        /// <param name="taxExemptIssuingJurisdiction">Indicates the jurisdiction in which the customer&#39;s tax exemption certificate was issued.  **Character limit**: 32   **Values**: a string of 32 characters or fewer .</param>
        /// <param name="taxExemptStatus"> Status of the account&#39;s tax exemption. This field is only required if you use Zuora Tax. This field is not available if you do not use Zuora Tax.   **Character limit**: 19   **Values**: one of the following:  - &#x60;Yes&#x60; - &#x60;No&#x60; - &#x60;PendingVerification&#x60; .</param>
        /// <param name="totalInvoiceBalance">Total balance of the account&#39;s invoices.  **Character limit**: 16   **Values**: a valid currency value .</param>
        /// <param name="vATId"> EU Value Added Tax ID. This feature is in **Limited Availability**. If you wish to have access to the feature, submit a request at [Zuora Global Support](http://support.zuora.com/).    **Character limit**: 25   **Values**: a valid Value Added Tax ID .</param>
        /// <param name="classNS">Value of the Class field for the corresponding customer account in NetSuite. Only available if you have installed the [Zuora Connector for NetSuite](https://www.zuora.com/connect/app/?appId&#x3D;265). .</param>
        /// <param name="customerTypeNS">Value of the Customer Type field for the corresponding customer account in NetSuite. The Customer Type field is used when the customer account is created in NetSuite. Only available if you have installed the [Zuora Connector for NetSuite](https://www.zuora.com/connect/app/?appId&#x3D;265). .</param>
        /// <param name="departmentNS">Value of the Department field for the corresponding customer account in NetSuite. Only available if you have installed the [Zuora Connector for NetSuite](https://www.zuora.com/connect/app/?appId&#x3D;265). .</param>
        /// <param name="integrationIdNS">ID of the corresponding object in NetSuite. Only available if you have installed the [Zuora Connector for NetSuite](https://www.zuora.com/connect/app/?appId&#x3D;265). .</param>
        /// <param name="integrationStatusNS">Status of the account&#39;s synchronization with NetSuite. Only available if you have installed the [Zuora Connector for NetSuite](https://www.zuora.com/connect/app/?appId&#x3D;265). .</param>
        /// <param name="locationNS">Value of the Location field for the corresponding customer account in NetSuite. Only available if you have installed the [Zuora Connector for NetSuite](https://www.zuora.com/connect/app/?appId&#x3D;265). .</param>
        /// <param name="subsidiaryNS">Value of the Subsidiary field for the corresponding customer account in NetSuite. The Subsidiary field is required if you use NetSuite OneWorld. Only available if you have installed the [Zuora Connector for NetSuite](https://www.zuora.com/connect/app/?appId&#x3D;265). .</param>
        /// <param name="syncDateNS">Date when the account was sychronized with NetSuite. Only available if you have installed the [Zuora Connector for NetSuite](https://www.zuora.com/connect/app/?appId&#x3D;265). .</param>
        /// <param name="synctoNetSuiteNS">Specifies whether the account should be synchronized with NetSuite. Only available if you have installed the [Zuora Connector for NetSuite](https://www.zuora.com/connect/app/?appId&#x3D;265). .</param>
        public SubscribeRequestAccount(string accountNumber = default(string), string additionalEmailAddresses = default(string), bool allowInvoiceEdit = default(bool), bool autoPay = default(bool), string batch = default(string), string bcdSettingOption = default(string), int billCycleDay = default(int), string communicationProfileId = default(string), string crmId = default(string), string currency = default(string), string customerServiceRepName = default(string), string defaultPaymentMethodId = default(string), string id = default(string), bool invoiceDeliveryPrefsEmail = default(bool), bool invoiceDeliveryPrefsPrint = default(bool), string invoiceTemplateId = default(string), DateTime lastInvoiceDate = default(DateTime), string name = default(string), string notes = default(string), string parentId = default(string), string paymentGateway = default(string), string paymentTerm = default(string), string purchaseOrderNumber = default(string), string salesRepName = default(string), string taxCompanyCode = default(string), string taxExemptCertificateID = default(string), string taxExemptCertificateType = default(string), string taxExemptDescription = default(string), DateTime taxExemptEffectiveDate = default(DateTime), DateTime taxExemptExpirationDate = default(DateTime), string taxExemptIssuingJurisdiction = default(string), string taxExemptStatus = default(string), double totalInvoiceBalance = default(double), string vATId = default(string), string classNS = default(string), CustomerTypeNSEnum? customerTypeNS = default(CustomerTypeNSEnum?), string departmentNS = default(string), string integrationIdNS = default(string), string integrationStatusNS = default(string), string locationNS = default(string), string subsidiaryNS = default(string), string syncDateNS = default(string), SynctoNetSuiteNSEnum? synctoNetSuiteNS = default(SynctoNetSuiteNSEnum?))
        {
            // to ensure "batch" is required (not null)
            if (batch == null)
            {
                throw new ArgumentNullException("batch is a required property for SubscribeRequestAccount and cannot be null");
            }
            this.Batch = batch;
            this.BillCycleDay = billCycleDay;
            // to ensure "currency" is required (not null)
            if (currency == null)
            {
                throw new ArgumentNullException("currency is a required property for SubscribeRequestAccount and cannot be null");
            }
            this.Currency = currency;
            // to ensure "name" is required (not null)
            if (name == null)
            {
                throw new ArgumentNullException("name is a required property for SubscribeRequestAccount and cannot be null");
            }
            this.Name = name;
            this.AccountNumber = accountNumber;
            this.AdditionalEmailAddresses = additionalEmailAddresses;
            this.AllowInvoiceEdit = allowInvoiceEdit;
            this.AutoPay = autoPay;
            this.BcdSettingOption = bcdSettingOption;
            this.CommunicationProfileId = communicationProfileId;
            this.CrmId = crmId;
            this.CustomerServiceRepName = customerServiceRepName;
            this.DefaultPaymentMethodId = defaultPaymentMethodId;
            this.Id = id;
            this.InvoiceDeliveryPrefsEmail = invoiceDeliveryPrefsEmail;
            this.InvoiceDeliveryPrefsPrint = invoiceDeliveryPrefsPrint;
            this.InvoiceTemplateId = invoiceTemplateId;
            this.LastInvoiceDate = lastInvoiceDate;
            this.Notes = notes;
            this.ParentId = parentId;
            this.PaymentGateway = paymentGateway;
            this.PaymentTerm = paymentTerm;
            this.PurchaseOrderNumber = purchaseOrderNumber;
            this.SalesRepName = salesRepName;
            this.TaxCompanyCode = taxCompanyCode;
            this.TaxExemptCertificateID = taxExemptCertificateID;
            this.TaxExemptCertificateType = taxExemptCertificateType;
            this.TaxExemptDescription = taxExemptDescription;
            this.TaxExemptEffectiveDate = taxExemptEffectiveDate;
            this.TaxExemptExpirationDate = taxExemptExpirationDate;
            this.TaxExemptIssuingJurisdiction = taxExemptIssuingJurisdiction;
            this.TaxExemptStatus = taxExemptStatus;
            this.TotalInvoiceBalance = totalInvoiceBalance;
            this.VATId = vATId;
            this.ClassNS = classNS;
            this.CustomerTypeNS = customerTypeNS;
            this.DepartmentNS = departmentNS;
            this.IntegrationIdNS = integrationIdNS;
            this.IntegrationStatusNS = integrationStatusNS;
            this.LocationNS = locationNS;
            this.SubsidiaryNS = subsidiaryNS;
            this.SyncDateNS = syncDateNS;
            this.SynctoNetSuiteNS = synctoNetSuiteNS;
        }

        /// <summary>
        /// Unique account number assigned to the account.  **Character limit**: 50   **Values**: one of the following:  - null to auto-generate - a string of 50 characters or fewer that doesn&#39;t begin with the default account number prefix 
        /// </summary>
        /// <value>Unique account number assigned to the account.  **Character limit**: 50   **Values**: one of the following:  - null to auto-generate - a string of 50 characters or fewer that doesn&#39;t begin with the default account number prefix </value>
        [DataMember(Name = "AccountNumber", EmitDefaultValue = false)]
        public string AccountNumber { get; set; }

        /// <summary>
        /// List of additional email addresses to receive emailed invoices.  **Character limit**: 120   **Values**: comma-separated list of email addresses 
        /// </summary>
        /// <value>List of additional email addresses to receive emailed invoices.  **Character limit**: 120   **Values**: comma-separated list of email addresses </value>
        [DataMember(Name = "AdditionalEmailAddresses", EmitDefaultValue = false)]
        public string AdditionalEmailAddresses { get; set; }

        /// <summary>
        ///  Indicates if associated invoices can be edited.   **Character limit**: 5   **Values**: &#x60;true&#x60;, &#x60;false&#x60; (default if left null) 
        /// </summary>
        /// <value> Indicates if associated invoices can be edited.   **Character limit**: 5   **Values**: &#x60;true&#x60;, &#x60;false&#x60; (default if left null) </value>
        [DataMember(Name = "AllowInvoiceEdit", EmitDefaultValue = true)]
        public bool AllowInvoiceEdit { get; set; }

        /// <summary>
        ///  Indicates if future payments are automatically collected when they&#39;re due during a Payment Run.   **Character limit**: 5   **Values**: &#x60;true&#x60;, &#x60;false&#x60; (default) 
        /// </summary>
        /// <value> Indicates if future payments are automatically collected when they&#39;re due during a Payment Run.   **Character limit**: 5   **Values**: &#x60;true&#x60;, &#x60;false&#x60; (default) </value>
        [DataMember(Name = "AutoPay", EmitDefaultValue = true)]
        public bool AutoPay { get; set; }

        /// <summary>
        ///  Organizes your customer accounts into groups to optimize your billing and payment operations.   **Character limit**: 20   **Values**:any system-defined batch (&#x60;Batch1&#x60; - &#x60;Batch50 &#x60;or by name). 
        /// </summary>
        /// <value> Organizes your customer accounts into groups to optimize your billing and payment operations.   **Character limit**: 20   **Values**:any system-defined batch (&#x60;Batch1&#x60; - &#x60;Batch50 &#x60;or by name). </value>
        [DataMember(Name = "Batch", IsRequired = true, EmitDefaultValue = false)]
        public string Batch { get; set; }

        /// <summary>
        /// Billing cycle day setting option.  **Character limit**: 9   **Values**: &#x60;AutoSet&#x60;, &#x60;ManualSet&#x60; 
        /// </summary>
        /// <value>Billing cycle day setting option.  **Character limit**: 9   **Values**: &#x60;AutoSet&#x60;, &#x60;ManualSet&#x60; </value>
        [DataMember(Name = "BcdSettingOption", EmitDefaultValue = false)]
        public string BcdSettingOption { get; set; }

        /// <summary>
        /// Billing cycle day (BCD) on which bill runs generate invoices for the account.  **Character limit**: 2   **Values**: any activated system-defined bill cycle day (&#x60;1&#x60; - &#x60;31&#x60;) 
        /// </summary>
        /// <value>Billing cycle day (BCD) on which bill runs generate invoices for the account.  **Character limit**: 2   **Values**: any activated system-defined bill cycle day (&#x60;1&#x60; - &#x60;31&#x60;) </value>
        [DataMember(Name = "BillCycleDay", IsRequired = true, EmitDefaultValue = false)]
        public int BillCycleDay { get; set; }

        /// <summary>
        /// Associates the account with a specified communication profile.  **Character limit**: 32   **Values**: a valid communication profile ID 
        /// </summary>
        /// <value>Associates the account with a specified communication profile.  **Character limit**: 32   **Values**: a valid communication profile ID </value>
        [DataMember(Name = "CommunicationProfileId", EmitDefaultValue = false)]
        public string CommunicationProfileId { get; set; }

        /// <summary>
        /// CRM account ID for the account. A CRM is a customer relationship management system, such as Salesforce.com.  **Character limit**: 100   **Values**: a string of 100 characters or fewer 
        /// </summary>
        /// <value>CRM account ID for the account. A CRM is a customer relationship management system, such as Salesforce.com.  **Character limit**: 100   **Values**: a string of 100 characters or fewer </value>
        [DataMember(Name = "CrmId", EmitDefaultValue = false)]
        public string CrmId { get; set; }

        /// <summary>
        ///  Currency that the customer is billed in. See [a currency value defined in the Zuora Ui admin settings](https://knowledgecenter.zuora.com/CB_Billing/Billing_Settings/Customize_Currencies) 
        /// </summary>
        /// <value> Currency that the customer is billed in. See [a currency value defined in the Zuora Ui admin settings](https://knowledgecenter.zuora.com/CB_Billing/Billing_Settings/Customize_Currencies) </value>
        [DataMember(Name = "Currency", IsRequired = true, EmitDefaultValue = false)]
        public string Currency { get; set; }

        /// <summary>
        /// Name of the account&#39;s customer service representative, if applicable.  **Character limit**: 50   **Values**: a string of 50 characters or fewer 
        /// </summary>
        /// <value>Name of the account&#39;s customer service representative, if applicable.  **Character limit**: 50   **Values**: a string of 50 characters or fewer </value>
        [DataMember(Name = "CustomerServiceRepName", EmitDefaultValue = false)]
        public string CustomerServiceRepName { get; set; }

        /// <summary>
        /// ID of the default payment method for the account. This field is only required if the &#x60;AutoPay&#x60; field is set to &#x60;true&#x60;. For a specified credit card payment method, it is recommended that [the support for stored credential transactions](https://knowledgecenter.zuora.com/Billing/Billing_and_Payments/L_Payment_Methods/Stored_credential_transactions) for this payment method is already enabled. **Character limit**: 32   **Values**: A valid ID for an existing payment method. This field does not support external payment methods. 
        /// </summary>
        /// <value>ID of the default payment method for the account. This field is only required if the &#x60;AutoPay&#x60; field is set to &#x60;true&#x60;. For a specified credit card payment method, it is recommended that [the support for stored credential transactions](https://knowledgecenter.zuora.com/Billing/Billing_and_Payments/L_Payment_Methods/Stored_credential_transactions) for this payment method is already enabled. **Character limit**: 32   **Values**: A valid ID for an existing payment method. This field does not support external payment methods. </value>
        [DataMember(Name = "DefaultPaymentMethodId", EmitDefaultValue = false)]
        public string DefaultPaymentMethodId { get; set; }

        /// <summary>
        /// Internal identifier of an existing account. Only set this field if you want to assign the subscription to an existing account.
        /// </summary>
        /// <value>Internal identifier of an existing account. Only set this field if you want to assign the subscription to an existing account.</value>
        [DataMember(Name = "Id", EmitDefaultValue = false)]
        public string Id { get; set; }

        /// <summary>
        /// Indicates if the customer wants to receive invoices through email. **Character limit**: 5   **Values**: &#x60;true&#x60;, &#x60;false&#x60; (default if left null) 
        /// </summary>
        /// <value>Indicates if the customer wants to receive invoices through email. **Character limit**: 5   **Values**: &#x60;true&#x60;, &#x60;false&#x60; (default if left null) </value>
        [DataMember(Name = "InvoiceDeliveryPrefsEmail", EmitDefaultValue = true)]
        public bool InvoiceDeliveryPrefsEmail { get; set; }

        /// <summary>
        /// Indicates if the customer wants to receive printed invoices, such as through postal mail.  **Character limit**: 5   **Values**: &#x60;true&#x60;, &#x60;false&#x60; (default if left null) 
        /// </summary>
        /// <value>Indicates if the customer wants to receive printed invoices, such as through postal mail.  **Character limit**: 5   **Values**: &#x60;true&#x60;, &#x60;false&#x60; (default if left null) </value>
        [DataMember(Name = "InvoiceDeliveryPrefsPrint", EmitDefaultValue = true)]
        public bool InvoiceDeliveryPrefsPrint { get; set; }

        /// <summary>
        /// The ID of the invoice template. Each customer account can use a specific invoice template for invoice generation.  **Character limit**: 32   **Values**: a[ valid template ID configured in Z-Billing Settings](https://knowledgecenter.zuora.com/CB_Billing/Billing_Settings/Manage_Invoice_Rules_and_Templates) To find the ID of your current invoice template: In Zuora, navigate to **Settings &gt; Z-Billing &gt; Manage Invoice Rules and Templates** and click **Show Id **next to the template you want to use.   
        /// </summary>
        /// <value>The ID of the invoice template. Each customer account can use a specific invoice template for invoice generation.  **Character limit**: 32   **Values**: a[ valid template ID configured in Z-Billing Settings](https://knowledgecenter.zuora.com/CB_Billing/Billing_Settings/Manage_Invoice_Rules_and_Templates) To find the ID of your current invoice template: In Zuora, navigate to **Settings &gt; Z-Billing &gt; Manage Invoice Rules and Templates** and click **Show Id **next to the template you want to use.   </value>
        [DataMember(Name = "InvoiceTemplateId", EmitDefaultValue = false)]
        public string InvoiceTemplateId { get; set; }

        /// <summary>
        ///  The date when the previous invoice was generated for the account. The field value is null if no invoice has ever been generated for the account.   **Character limit**: 29   **Values**: automatically generated 
        /// </summary>
        /// <value> The date when the previous invoice was generated for the account. The field value is null if no invoice has ever been generated for the account.   **Character limit**: 29   **Values**: automatically generated </value>
        [DataMember(Name = "LastInvoiceDate", EmitDefaultValue = false)]
        [JsonConverter(typeof(OpenAPIDateConverter))]
        public DateTime LastInvoiceDate { get; set; }

        /// <summary>
        /// Name of the account as displayed in the Zuora UI.  **Character limit**: 255   **Values**: a string of 255 characters or fewer 
        /// </summary>
        /// <value>Name of the account as displayed in the Zuora UI.  **Character limit**: 255   **Values**: a string of 255 characters or fewer </value>
        [DataMember(Name = "Name", IsRequired = true, EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        ///  Comments about the account.  **Character limit**: 65,535   **Values**: a string of 65,535 characters 
        /// </summary>
        /// <value> Comments about the account.  **Character limit**: 65,535   **Values**: a string of 65,535 characters </value>
        [DataMember(Name = "Notes", EmitDefaultValue = false)]
        public string Notes { get; set; }

        /// <summary>
        /// Identifier of the parent customer account for this Account object. Use this field if you have customer hierarchy enabled.  **Character limit**: 32   **Values**: a valid account ID 
        /// </summary>
        /// <value>Identifier of the parent customer account for this Account object. Use this field if you have customer hierarchy enabled.  **Character limit**: 32   **Values**: a valid account ID </value>
        [DataMember(Name = "ParentId", EmitDefaultValue = false)]
        public string ParentId { get; set; }

        /// <summary>
        /// Gateway used for processing electronic payments and refunds. This field is only required if there is no default payment gateway is defined in the tenant.  **Character limit**: 40   **Values**: one of the following:  - a valid configured gateway name - Null to inherit the default value set in Z-Payment Settings 
        /// </summary>
        /// <value>Gateway used for processing electronic payments and refunds. This field is only required if there is no default payment gateway is defined in the tenant.  **Character limit**: 40   **Values**: one of the following:  - a valid configured gateway name - Null to inherit the default value set in Z-Payment Settings </value>
        [DataMember(Name = "PaymentGateway", EmitDefaultValue = false)]
        public string PaymentGateway { get; set; }

        /// <summary>
        /// Indicates when the customer pays for subscriptions.  **Character limit**: 100   **Values**: [a valid, active payment term defined in the web-based UI administrative settings](https://knowledgecenter.zuora.com/CB_Billing/Billing_Settings/Define_Payment_Terms) 
        /// </summary>
        /// <value>Indicates when the customer pays for subscriptions.  **Character limit**: 100   **Values**: [a valid, active payment term defined in the web-based UI administrative settings](https://knowledgecenter.zuora.com/CB_Billing/Billing_Settings/Define_Payment_Terms) </value>
        [DataMember(Name = "PaymentTerm", EmitDefaultValue = false)]
        public string PaymentTerm { get; set; }

        /// <summary>
        /// The number of the purchase order associated with this account. Purchase order information generally comes from customers.  **Character limit**: 100   **Values**: a string of 100 characters or fewer 
        /// </summary>
        /// <value>The number of the purchase order associated with this account. Purchase order information generally comes from customers.  **Character limit**: 100   **Values**: a string of 100 characters or fewer </value>
        [DataMember(Name = "PurchaseOrderNumber", EmitDefaultValue = false)]
        public string PurchaseOrderNumber { get; set; }

        /// <summary>
        /// The name of the sales representative associated with this account, if applicable.  **Character limit**: 50   **Values**: a string of 50 characters or fewer 
        /// </summary>
        /// <value>The name of the sales representative associated with this account, if applicable.  **Character limit**: 50   **Values**: a string of 50 characters or fewer </value>
        [DataMember(Name = "SalesRepName", EmitDefaultValue = false)]
        public string SalesRepName { get; set; }

        /// <summary>
        ///  Unique code that identifies a company account in Avalara. Use this field to calculate taxes based on origin and sold-to addresses in Avalara. This feature is in **Limited Availability**. If you wish to have access to the feature, submit a request at [Zuora Global Support](http://support.zuora.com/).    **Character limit**: 50   **Values**: a valid company code 
        /// </summary>
        /// <value> Unique code that identifies a company account in Avalara. Use this field to calculate taxes based on origin and sold-to addresses in Avalara. This feature is in **Limited Availability**. If you wish to have access to the feature, submit a request at [Zuora Global Support](http://support.zuora.com/).    **Character limit**: 50   **Values**: a valid company code </value>
        [DataMember(Name = "TaxCompanyCode", EmitDefaultValue = false)]
        public string TaxCompanyCode { get; set; }

        /// <summary>
        /// ID of your customer&#39;s tax exemption certificate.  **Character limit**: 32   **Values**: a string of 32 characters or fewer 
        /// </summary>
        /// <value>ID of your customer&#39;s tax exemption certificate.  **Character limit**: 32   **Values**: a string of 32 characters or fewer </value>
        [DataMember(Name = "TaxExemptCertificateID", EmitDefaultValue = false)]
        public string TaxExemptCertificateID { get; set; }

        /// <summary>
        /// Type of the tax exemption certificate that your customer holds. **Character limit**: 32   **Values**: a string of 32 characters or fewer 
        /// </summary>
        /// <value>Type of the tax exemption certificate that your customer holds. **Character limit**: 32   **Values**: a string of 32 characters or fewer </value>
        [DataMember(Name = "TaxExemptCertificateType", EmitDefaultValue = false)]
        public string TaxExemptCertificateType { get; set; }

        /// <summary>
        /// Description of the tax exemption certificate that your customer holds.  **Character limit**: 500   **Values**: a string of 500 characters or fewer 
        /// </summary>
        /// <value>Description of the tax exemption certificate that your customer holds.  **Character limit**: 500   **Values**: a string of 500 characters or fewer </value>
        [DataMember(Name = "TaxExemptDescription", EmitDefaultValue = false)]
        public string TaxExemptDescription { get; set; }

        /// <summary>
        /// Date when the the customer&#39;s tax exemption starts.  **Character limit**: 29 **Version notes**: requires Z-Tax 
        /// </summary>
        /// <value>Date when the the customer&#39;s tax exemption starts.  **Character limit**: 29 **Version notes**: requires Z-Tax </value>
        [DataMember(Name = "TaxExemptEffectiveDate", EmitDefaultValue = false)]
        [JsonConverter(typeof(OpenAPIDateConverter))]
        public DateTime TaxExemptEffectiveDate { get; set; }

        /// <summary>
        /// Date when the customer&#39;s tax exemption certificate expires **Character limit**: 29 **Version notes**: requires Z-Tax 
        /// </summary>
        /// <value>Date when the customer&#39;s tax exemption certificate expires **Character limit**: 29 **Version notes**: requires Z-Tax </value>
        [DataMember(Name = "TaxExemptExpirationDate", EmitDefaultValue = false)]
        [JsonConverter(typeof(OpenAPIDateConverter))]
        public DateTime TaxExemptExpirationDate { get; set; }

        /// <summary>
        /// Indicates the jurisdiction in which the customer&#39;s tax exemption certificate was issued.  **Character limit**: 32   **Values**: a string of 32 characters or fewer 
        /// </summary>
        /// <value>Indicates the jurisdiction in which the customer&#39;s tax exemption certificate was issued.  **Character limit**: 32   **Values**: a string of 32 characters or fewer </value>
        [DataMember(Name = "TaxExemptIssuingJurisdiction", EmitDefaultValue = false)]
        public string TaxExemptIssuingJurisdiction { get; set; }

        /// <summary>
        ///  Status of the account&#39;s tax exemption. This field is only required if you use Zuora Tax. This field is not available if you do not use Zuora Tax.   **Character limit**: 19   **Values**: one of the following:  - &#x60;Yes&#x60; - &#x60;No&#x60; - &#x60;PendingVerification&#x60; 
        /// </summary>
        /// <value> Status of the account&#39;s tax exemption. This field is only required if you use Zuora Tax. This field is not available if you do not use Zuora Tax.   **Character limit**: 19   **Values**: one of the following:  - &#x60;Yes&#x60; - &#x60;No&#x60; - &#x60;PendingVerification&#x60; </value>
        [DataMember(Name = "TaxExemptStatus", EmitDefaultValue = false)]
        public string TaxExemptStatus { get; set; }

        /// <summary>
        /// Total balance of the account&#39;s invoices.  **Character limit**: 16   **Values**: a valid currency value 
        /// </summary>
        /// <value>Total balance of the account&#39;s invoices.  **Character limit**: 16   **Values**: a valid currency value </value>
        [DataMember(Name = "TotalInvoiceBalance", EmitDefaultValue = false)]
        public double TotalInvoiceBalance { get; set; }

        /// <summary>
        ///  EU Value Added Tax ID. This feature is in **Limited Availability**. If you wish to have access to the feature, submit a request at [Zuora Global Support](http://support.zuora.com/).    **Character limit**: 25   **Values**: a valid Value Added Tax ID 
        /// </summary>
        /// <value> EU Value Added Tax ID. This feature is in **Limited Availability**. If you wish to have access to the feature, submit a request at [Zuora Global Support](http://support.zuora.com/).    **Character limit**: 25   **Values**: a valid Value Added Tax ID </value>
        [DataMember(Name = "VATId", EmitDefaultValue = false)]
        public string VATId { get; set; }

        /// <summary>
        /// Value of the Class field for the corresponding customer account in NetSuite. Only available if you have installed the [Zuora Connector for NetSuite](https://www.zuora.com/connect/app/?appId&#x3D;265). 
        /// </summary>
        /// <value>Value of the Class field for the corresponding customer account in NetSuite. Only available if you have installed the [Zuora Connector for NetSuite](https://www.zuora.com/connect/app/?appId&#x3D;265). </value>
        [DataMember(Name = "Class__NS", EmitDefaultValue = false)]
        public string ClassNS { get; set; }

        /// <summary>
        /// Value of the Department field for the corresponding customer account in NetSuite. Only available if you have installed the [Zuora Connector for NetSuite](https://www.zuora.com/connect/app/?appId&#x3D;265). 
        /// </summary>
        /// <value>Value of the Department field for the corresponding customer account in NetSuite. Only available if you have installed the [Zuora Connector for NetSuite](https://www.zuora.com/connect/app/?appId&#x3D;265). </value>
        [DataMember(Name = "Department__NS", EmitDefaultValue = false)]
        public string DepartmentNS { get; set; }

        /// <summary>
        /// ID of the corresponding object in NetSuite. Only available if you have installed the [Zuora Connector for NetSuite](https://www.zuora.com/connect/app/?appId&#x3D;265). 
        /// </summary>
        /// <value>ID of the corresponding object in NetSuite. Only available if you have installed the [Zuora Connector for NetSuite](https://www.zuora.com/connect/app/?appId&#x3D;265). </value>
        [DataMember(Name = "IntegrationId__NS", EmitDefaultValue = false)]
        public string IntegrationIdNS { get; set; }

        /// <summary>
        /// Status of the account&#39;s synchronization with NetSuite. Only available if you have installed the [Zuora Connector for NetSuite](https://www.zuora.com/connect/app/?appId&#x3D;265). 
        /// </summary>
        /// <value>Status of the account&#39;s synchronization with NetSuite. Only available if you have installed the [Zuora Connector for NetSuite](https://www.zuora.com/connect/app/?appId&#x3D;265). </value>
        [DataMember(Name = "IntegrationStatus__NS", EmitDefaultValue = false)]
        public string IntegrationStatusNS { get; set; }

        /// <summary>
        /// Value of the Location field for the corresponding customer account in NetSuite. Only available if you have installed the [Zuora Connector for NetSuite](https://www.zuora.com/connect/app/?appId&#x3D;265). 
        /// </summary>
        /// <value>Value of the Location field for the corresponding customer account in NetSuite. Only available if you have installed the [Zuora Connector for NetSuite](https://www.zuora.com/connect/app/?appId&#x3D;265). </value>
        [DataMember(Name = "Location__NS", EmitDefaultValue = false)]
        public string LocationNS { get; set; }

        /// <summary>
        /// Value of the Subsidiary field for the corresponding customer account in NetSuite. The Subsidiary field is required if you use NetSuite OneWorld. Only available if you have installed the [Zuora Connector for NetSuite](https://www.zuora.com/connect/app/?appId&#x3D;265). 
        /// </summary>
        /// <value>Value of the Subsidiary field for the corresponding customer account in NetSuite. The Subsidiary field is required if you use NetSuite OneWorld. Only available if you have installed the [Zuora Connector for NetSuite](https://www.zuora.com/connect/app/?appId&#x3D;265). </value>
        [DataMember(Name = "Subsidiary__NS", EmitDefaultValue = false)]
        public string SubsidiaryNS { get; set; }

        /// <summary>
        /// Date when the account was sychronized with NetSuite. Only available if you have installed the [Zuora Connector for NetSuite](https://www.zuora.com/connect/app/?appId&#x3D;265). 
        /// </summary>
        /// <value>Date when the account was sychronized with NetSuite. Only available if you have installed the [Zuora Connector for NetSuite](https://www.zuora.com/connect/app/?appId&#x3D;265). </value>
        [DataMember(Name = "SyncDate__NS", EmitDefaultValue = false)]
        public string SyncDateNS { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class SubscribeRequestAccount {\n");
            sb.Append("  AccountNumber: ").Append(AccountNumber).Append("\n");
            sb.Append("  AdditionalEmailAddresses: ").Append(AdditionalEmailAddresses).Append("\n");
            sb.Append("  AllowInvoiceEdit: ").Append(AllowInvoiceEdit).Append("\n");
            sb.Append("  AutoPay: ").Append(AutoPay).Append("\n");
            sb.Append("  Batch: ").Append(Batch).Append("\n");
            sb.Append("  BcdSettingOption: ").Append(BcdSettingOption).Append("\n");
            sb.Append("  BillCycleDay: ").Append(BillCycleDay).Append("\n");
            sb.Append("  CommunicationProfileId: ").Append(CommunicationProfileId).Append("\n");
            sb.Append("  CrmId: ").Append(CrmId).Append("\n");
            sb.Append("  Currency: ").Append(Currency).Append("\n");
            sb.Append("  CustomerServiceRepName: ").Append(CustomerServiceRepName).Append("\n");
            sb.Append("  DefaultPaymentMethodId: ").Append(DefaultPaymentMethodId).Append("\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  InvoiceDeliveryPrefsEmail: ").Append(InvoiceDeliveryPrefsEmail).Append("\n");
            sb.Append("  InvoiceDeliveryPrefsPrint: ").Append(InvoiceDeliveryPrefsPrint).Append("\n");
            sb.Append("  InvoiceTemplateId: ").Append(InvoiceTemplateId).Append("\n");
            sb.Append("  LastInvoiceDate: ").Append(LastInvoiceDate).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Notes: ").Append(Notes).Append("\n");
            sb.Append("  ParentId: ").Append(ParentId).Append("\n");
            sb.Append("  PaymentGateway: ").Append(PaymentGateway).Append("\n");
            sb.Append("  PaymentTerm: ").Append(PaymentTerm).Append("\n");
            sb.Append("  PurchaseOrderNumber: ").Append(PurchaseOrderNumber).Append("\n");
            sb.Append("  SalesRepName: ").Append(SalesRepName).Append("\n");
            sb.Append("  TaxCompanyCode: ").Append(TaxCompanyCode).Append("\n");
            sb.Append("  TaxExemptCertificateID: ").Append(TaxExemptCertificateID).Append("\n");
            sb.Append("  TaxExemptCertificateType: ").Append(TaxExemptCertificateType).Append("\n");
            sb.Append("  TaxExemptDescription: ").Append(TaxExemptDescription).Append("\n");
            sb.Append("  TaxExemptEffectiveDate: ").Append(TaxExemptEffectiveDate).Append("\n");
            sb.Append("  TaxExemptExpirationDate: ").Append(TaxExemptExpirationDate).Append("\n");
            sb.Append("  TaxExemptIssuingJurisdiction: ").Append(TaxExemptIssuingJurisdiction).Append("\n");
            sb.Append("  TaxExemptStatus: ").Append(TaxExemptStatus).Append("\n");
            sb.Append("  TotalInvoiceBalance: ").Append(TotalInvoiceBalance).Append("\n");
            sb.Append("  VATId: ").Append(VATId).Append("\n");
            sb.Append("  ClassNS: ").Append(ClassNS).Append("\n");
            sb.Append("  CustomerTypeNS: ").Append(CustomerTypeNS).Append("\n");
            sb.Append("  DepartmentNS: ").Append(DepartmentNS).Append("\n");
            sb.Append("  IntegrationIdNS: ").Append(IntegrationIdNS).Append("\n");
            sb.Append("  IntegrationStatusNS: ").Append(IntegrationStatusNS).Append("\n");
            sb.Append("  LocationNS: ").Append(LocationNS).Append("\n");
            sb.Append("  SubsidiaryNS: ").Append(SubsidiaryNS).Append("\n");
            sb.Append("  SyncDateNS: ").Append(SyncDateNS).Append("\n");
            sb.Append("  SynctoNetSuiteNS: ").Append(SynctoNetSuiteNS).Append("\n");
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
            return this.Equals(input as SubscribeRequestAccount);
        }

        /// <summary>
        /// Returns true if SubscribeRequestAccount instances are equal
        /// </summary>
        /// <param name="input">Instance of SubscribeRequestAccount to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(SubscribeRequestAccount input)
        {
            if (input == null)
            {
                return false;
            }
            return 
                (
                    this.AccountNumber == input.AccountNumber ||
                    (this.AccountNumber != null &&
                    this.AccountNumber.Equals(input.AccountNumber))
                ) && 
                (
                    this.AdditionalEmailAddresses == input.AdditionalEmailAddresses ||
                    (this.AdditionalEmailAddresses != null &&
                    this.AdditionalEmailAddresses.Equals(input.AdditionalEmailAddresses))
                ) && 
                (
                    this.AllowInvoiceEdit == input.AllowInvoiceEdit ||
                    this.AllowInvoiceEdit.Equals(input.AllowInvoiceEdit)
                ) && 
                (
                    this.AutoPay == input.AutoPay ||
                    this.AutoPay.Equals(input.AutoPay)
                ) && 
                (
                    this.Batch == input.Batch ||
                    (this.Batch != null &&
                    this.Batch.Equals(input.Batch))
                ) && 
                (
                    this.BcdSettingOption == input.BcdSettingOption ||
                    (this.BcdSettingOption != null &&
                    this.BcdSettingOption.Equals(input.BcdSettingOption))
                ) && 
                (
                    this.BillCycleDay == input.BillCycleDay ||
                    this.BillCycleDay.Equals(input.BillCycleDay)
                ) && 
                (
                    this.CommunicationProfileId == input.CommunicationProfileId ||
                    (this.CommunicationProfileId != null &&
                    this.CommunicationProfileId.Equals(input.CommunicationProfileId))
                ) && 
                (
                    this.CrmId == input.CrmId ||
                    (this.CrmId != null &&
                    this.CrmId.Equals(input.CrmId))
                ) && 
                (
                    this.Currency == input.Currency ||
                    (this.Currency != null &&
                    this.Currency.Equals(input.Currency))
                ) && 
                (
                    this.CustomerServiceRepName == input.CustomerServiceRepName ||
                    (this.CustomerServiceRepName != null &&
                    this.CustomerServiceRepName.Equals(input.CustomerServiceRepName))
                ) && 
                (
                    this.DefaultPaymentMethodId == input.DefaultPaymentMethodId ||
                    (this.DefaultPaymentMethodId != null &&
                    this.DefaultPaymentMethodId.Equals(input.DefaultPaymentMethodId))
                ) && 
                (
                    this.Id == input.Id ||
                    (this.Id != null &&
                    this.Id.Equals(input.Id))
                ) && 
                (
                    this.InvoiceDeliveryPrefsEmail == input.InvoiceDeliveryPrefsEmail ||
                    this.InvoiceDeliveryPrefsEmail.Equals(input.InvoiceDeliveryPrefsEmail)
                ) && 
                (
                    this.InvoiceDeliveryPrefsPrint == input.InvoiceDeliveryPrefsPrint ||
                    this.InvoiceDeliveryPrefsPrint.Equals(input.InvoiceDeliveryPrefsPrint)
                ) && 
                (
                    this.InvoiceTemplateId == input.InvoiceTemplateId ||
                    (this.InvoiceTemplateId != null &&
                    this.InvoiceTemplateId.Equals(input.InvoiceTemplateId))
                ) && 
                (
                    this.LastInvoiceDate == input.LastInvoiceDate ||
                    (this.LastInvoiceDate != null &&
                    this.LastInvoiceDate.Equals(input.LastInvoiceDate))
                ) && 
                (
                    this.Name == input.Name ||
                    (this.Name != null &&
                    this.Name.Equals(input.Name))
                ) && 
                (
                    this.Notes == input.Notes ||
                    (this.Notes != null &&
                    this.Notes.Equals(input.Notes))
                ) && 
                (
                    this.ParentId == input.ParentId ||
                    (this.ParentId != null &&
                    this.ParentId.Equals(input.ParentId))
                ) && 
                (
                    this.PaymentGateway == input.PaymentGateway ||
                    (this.PaymentGateway != null &&
                    this.PaymentGateway.Equals(input.PaymentGateway))
                ) && 
                (
                    this.PaymentTerm == input.PaymentTerm ||
                    (this.PaymentTerm != null &&
                    this.PaymentTerm.Equals(input.PaymentTerm))
                ) && 
                (
                    this.PurchaseOrderNumber == input.PurchaseOrderNumber ||
                    (this.PurchaseOrderNumber != null &&
                    this.PurchaseOrderNumber.Equals(input.PurchaseOrderNumber))
                ) && 
                (
                    this.SalesRepName == input.SalesRepName ||
                    (this.SalesRepName != null &&
                    this.SalesRepName.Equals(input.SalesRepName))
                ) && 
                (
                    this.TaxCompanyCode == input.TaxCompanyCode ||
                    (this.TaxCompanyCode != null &&
                    this.TaxCompanyCode.Equals(input.TaxCompanyCode))
                ) && 
                (
                    this.TaxExemptCertificateID == input.TaxExemptCertificateID ||
                    (this.TaxExemptCertificateID != null &&
                    this.TaxExemptCertificateID.Equals(input.TaxExemptCertificateID))
                ) && 
                (
                    this.TaxExemptCertificateType == input.TaxExemptCertificateType ||
                    (this.TaxExemptCertificateType != null &&
                    this.TaxExemptCertificateType.Equals(input.TaxExemptCertificateType))
                ) && 
                (
                    this.TaxExemptDescription == input.TaxExemptDescription ||
                    (this.TaxExemptDescription != null &&
                    this.TaxExemptDescription.Equals(input.TaxExemptDescription))
                ) && 
                (
                    this.TaxExemptEffectiveDate == input.TaxExemptEffectiveDate ||
                    (this.TaxExemptEffectiveDate != null &&
                    this.TaxExemptEffectiveDate.Equals(input.TaxExemptEffectiveDate))
                ) && 
                (
                    this.TaxExemptExpirationDate == input.TaxExemptExpirationDate ||
                    (this.TaxExemptExpirationDate != null &&
                    this.TaxExemptExpirationDate.Equals(input.TaxExemptExpirationDate))
                ) && 
                (
                    this.TaxExemptIssuingJurisdiction == input.TaxExemptIssuingJurisdiction ||
                    (this.TaxExemptIssuingJurisdiction != null &&
                    this.TaxExemptIssuingJurisdiction.Equals(input.TaxExemptIssuingJurisdiction))
                ) && 
                (
                    this.TaxExemptStatus == input.TaxExemptStatus ||
                    (this.TaxExemptStatus != null &&
                    this.TaxExemptStatus.Equals(input.TaxExemptStatus))
                ) && 
                (
                    this.TotalInvoiceBalance == input.TotalInvoiceBalance ||
                    this.TotalInvoiceBalance.Equals(input.TotalInvoiceBalance)
                ) && 
                (
                    this.VATId == input.VATId ||
                    (this.VATId != null &&
                    this.VATId.Equals(input.VATId))
                ) && 
                (
                    this.ClassNS == input.ClassNS ||
                    (this.ClassNS != null &&
                    this.ClassNS.Equals(input.ClassNS))
                ) && 
                (
                    this.CustomerTypeNS == input.CustomerTypeNS ||
                    this.CustomerTypeNS.Equals(input.CustomerTypeNS)
                ) && 
                (
                    this.DepartmentNS == input.DepartmentNS ||
                    (this.DepartmentNS != null &&
                    this.DepartmentNS.Equals(input.DepartmentNS))
                ) && 
                (
                    this.IntegrationIdNS == input.IntegrationIdNS ||
                    (this.IntegrationIdNS != null &&
                    this.IntegrationIdNS.Equals(input.IntegrationIdNS))
                ) && 
                (
                    this.IntegrationStatusNS == input.IntegrationStatusNS ||
                    (this.IntegrationStatusNS != null &&
                    this.IntegrationStatusNS.Equals(input.IntegrationStatusNS))
                ) && 
                (
                    this.LocationNS == input.LocationNS ||
                    (this.LocationNS != null &&
                    this.LocationNS.Equals(input.LocationNS))
                ) && 
                (
                    this.SubsidiaryNS == input.SubsidiaryNS ||
                    (this.SubsidiaryNS != null &&
                    this.SubsidiaryNS.Equals(input.SubsidiaryNS))
                ) && 
                (
                    this.SyncDateNS == input.SyncDateNS ||
                    (this.SyncDateNS != null &&
                    this.SyncDateNS.Equals(input.SyncDateNS))
                ) && 
                (
                    this.SynctoNetSuiteNS == input.SynctoNetSuiteNS ||
                    this.SynctoNetSuiteNS.Equals(input.SynctoNetSuiteNS)
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
                if (this.AccountNumber != null)
                {
                    hashCode = (hashCode * 59) + this.AccountNumber.GetHashCode();
                }
                if (this.AdditionalEmailAddresses != null)
                {
                    hashCode = (hashCode * 59) + this.AdditionalEmailAddresses.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.AllowInvoiceEdit.GetHashCode();
                hashCode = (hashCode * 59) + this.AutoPay.GetHashCode();
                if (this.Batch != null)
                {
                    hashCode = (hashCode * 59) + this.Batch.GetHashCode();
                }
                if (this.BcdSettingOption != null)
                {
                    hashCode = (hashCode * 59) + this.BcdSettingOption.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.BillCycleDay.GetHashCode();
                if (this.CommunicationProfileId != null)
                {
                    hashCode = (hashCode * 59) + this.CommunicationProfileId.GetHashCode();
                }
                if (this.CrmId != null)
                {
                    hashCode = (hashCode * 59) + this.CrmId.GetHashCode();
                }
                if (this.Currency != null)
                {
                    hashCode = (hashCode * 59) + this.Currency.GetHashCode();
                }
                if (this.CustomerServiceRepName != null)
                {
                    hashCode = (hashCode * 59) + this.CustomerServiceRepName.GetHashCode();
                }
                if (this.DefaultPaymentMethodId != null)
                {
                    hashCode = (hashCode * 59) + this.DefaultPaymentMethodId.GetHashCode();
                }
                if (this.Id != null)
                {
                    hashCode = (hashCode * 59) + this.Id.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.InvoiceDeliveryPrefsEmail.GetHashCode();
                hashCode = (hashCode * 59) + this.InvoiceDeliveryPrefsPrint.GetHashCode();
                if (this.InvoiceTemplateId != null)
                {
                    hashCode = (hashCode * 59) + this.InvoiceTemplateId.GetHashCode();
                }
                if (this.LastInvoiceDate != null)
                {
                    hashCode = (hashCode * 59) + this.LastInvoiceDate.GetHashCode();
                }
                if (this.Name != null)
                {
                    hashCode = (hashCode * 59) + this.Name.GetHashCode();
                }
                if (this.Notes != null)
                {
                    hashCode = (hashCode * 59) + this.Notes.GetHashCode();
                }
                if (this.ParentId != null)
                {
                    hashCode = (hashCode * 59) + this.ParentId.GetHashCode();
                }
                if (this.PaymentGateway != null)
                {
                    hashCode = (hashCode * 59) + this.PaymentGateway.GetHashCode();
                }
                if (this.PaymentTerm != null)
                {
                    hashCode = (hashCode * 59) + this.PaymentTerm.GetHashCode();
                }
                if (this.PurchaseOrderNumber != null)
                {
                    hashCode = (hashCode * 59) + this.PurchaseOrderNumber.GetHashCode();
                }
                if (this.SalesRepName != null)
                {
                    hashCode = (hashCode * 59) + this.SalesRepName.GetHashCode();
                }
                if (this.TaxCompanyCode != null)
                {
                    hashCode = (hashCode * 59) + this.TaxCompanyCode.GetHashCode();
                }
                if (this.TaxExemptCertificateID != null)
                {
                    hashCode = (hashCode * 59) + this.TaxExemptCertificateID.GetHashCode();
                }
                if (this.TaxExemptCertificateType != null)
                {
                    hashCode = (hashCode * 59) + this.TaxExemptCertificateType.GetHashCode();
                }
                if (this.TaxExemptDescription != null)
                {
                    hashCode = (hashCode * 59) + this.TaxExemptDescription.GetHashCode();
                }
                if (this.TaxExemptEffectiveDate != null)
                {
                    hashCode = (hashCode * 59) + this.TaxExemptEffectiveDate.GetHashCode();
                }
                if (this.TaxExemptExpirationDate != null)
                {
                    hashCode = (hashCode * 59) + this.TaxExemptExpirationDate.GetHashCode();
                }
                if (this.TaxExemptIssuingJurisdiction != null)
                {
                    hashCode = (hashCode * 59) + this.TaxExemptIssuingJurisdiction.GetHashCode();
                }
                if (this.TaxExemptStatus != null)
                {
                    hashCode = (hashCode * 59) + this.TaxExemptStatus.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.TotalInvoiceBalance.GetHashCode();
                if (this.VATId != null)
                {
                    hashCode = (hashCode * 59) + this.VATId.GetHashCode();
                }
                if (this.ClassNS != null)
                {
                    hashCode = (hashCode * 59) + this.ClassNS.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.CustomerTypeNS.GetHashCode();
                if (this.DepartmentNS != null)
                {
                    hashCode = (hashCode * 59) + this.DepartmentNS.GetHashCode();
                }
                if (this.IntegrationIdNS != null)
                {
                    hashCode = (hashCode * 59) + this.IntegrationIdNS.GetHashCode();
                }
                if (this.IntegrationStatusNS != null)
                {
                    hashCode = (hashCode * 59) + this.IntegrationStatusNS.GetHashCode();
                }
                if (this.LocationNS != null)
                {
                    hashCode = (hashCode * 59) + this.LocationNS.GetHashCode();
                }
                if (this.SubsidiaryNS != null)
                {
                    hashCode = (hashCode * 59) + this.SubsidiaryNS.GetHashCode();
                }
                if (this.SyncDateNS != null)
                {
                    hashCode = (hashCode * 59) + this.SyncDateNS.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.SynctoNetSuiteNS.GetHashCode();
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
            // ClassNS (string) maxLength
            if (this.ClassNS != null && this.ClassNS.Length > 255)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for ClassNS, length must be less than 255.", new [] { "ClassNS" });
            }

            // DepartmentNS (string) maxLength
            if (this.DepartmentNS != null && this.DepartmentNS.Length > 255)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for DepartmentNS, length must be less than 255.", new [] { "DepartmentNS" });
            }

            // IntegrationIdNS (string) maxLength
            if (this.IntegrationIdNS != null && this.IntegrationIdNS.Length > 255)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for IntegrationIdNS, length must be less than 255.", new [] { "IntegrationIdNS" });
            }

            // IntegrationStatusNS (string) maxLength
            if (this.IntegrationStatusNS != null && this.IntegrationStatusNS.Length > 255)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for IntegrationStatusNS, length must be less than 255.", new [] { "IntegrationStatusNS" });
            }

            // LocationNS (string) maxLength
            if (this.LocationNS != null && this.LocationNS.Length > 255)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for LocationNS, length must be less than 255.", new [] { "LocationNS" });
            }

            // SubsidiaryNS (string) maxLength
            if (this.SubsidiaryNS != null && this.SubsidiaryNS.Length > 255)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for SubsidiaryNS, length must be less than 255.", new [] { "SubsidiaryNS" });
            }

            // SyncDateNS (string) maxLength
            if (this.SyncDateNS != null && this.SyncDateNS.Length > 255)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for SyncDateNS, length must be less than 255.", new [] { "SyncDateNS" });
            }

            yield break;
        }
    }

}
