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

import { PagedRequest, TenantSummaryPagedResponse } from "./data-contracts";
import { ContentType, HttpClient, RequestParams } from "./http-client";

export class Tenant<SecurityDataType = unknown> extends HttpClient<SecurityDataType> {
  /**
   * No description
   *
   * @tags Tenant
   * @name GetPublicTenants
   * @request POST:/api/tenant/list
   */
  getPublicTenants = (data: PagedRequest, params: RequestParams = {}) =>
    this.request<TenantSummaryPagedResponse, any>({
      path: `/api/tenant/list`,
      method: "POST",
      body: data,
      type: ContentType.Json,
      format: "json",
      ...params,
    });
}
