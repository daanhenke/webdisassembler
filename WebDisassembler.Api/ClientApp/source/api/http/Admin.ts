/* eslint-disable */
/* tslint:disable */
/*
 * ---------------------------------------------------------------
 * ## THIS FILE WAS GENERATED VIA SWAGGER-TYPESCRIPT-API        ##
 * ##                                                           ##
 * ## AUTHOR: acacode                                           ##
 * ## SOURCE: https://github.com/acacode/swagger-typescript-api ##
 * ---------------------------------------------------------------
 */

import { CreateTenant } from "./data-contracts";
import { ContentType, HttpClient, RequestParams } from "./http-client";

export class Admin<SecurityDataType = unknown> extends HttpClient<SecurityDataType> {
  /**
   * No description
   *
   * @tags Search
   * @name ReindexAll
   * @request GET:/api/admin/search/reindex/all
   */
  reindexAll = (params: RequestParams = {}) =>
    this.request<void, any>({
      path: `/api/admin/search/reindex/all`,
      method: "GET",
      ...params,
    });
  /**
   * No description
   *
   * @tags TenantAdmin
   * @name CreateDebugTenant
   * @request POST:/api/admin/tenant/create
   */
  createDebugTenant = (data: CreateTenant, params: RequestParams = {}) =>
    this.request<void, any>({
      path: `/api/admin/tenant/create`,
      method: "POST",
      body: data,
      type: ContentType.Json,
      ...params,
    });
}
