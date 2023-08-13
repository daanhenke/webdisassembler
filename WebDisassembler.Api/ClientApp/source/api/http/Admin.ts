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

import {
  CreateAdminUser,
  CreateTenant,
  QueryRequest,
  TenantSummaryPagedResponse,
  UserSummaryPagedResponse,
} from "./data-contracts";
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
  /**
   * No description
   *
   * @tags TenantAdmin
   * @name ListTenants
   * @request POST:/api/admin/tenant/list
   */
  listTenants = (data: QueryRequest, params: RequestParams = {}) =>
    this.request<TenantSummaryPagedResponse, any>({
      path: `/api/admin/tenant/list`,
      method: "POST",
      body: data,
      type: ContentType.Json,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags UserAdmin
   * @name CreateAdminUser
   * @request POST:/api/admin/admin/create
   */
  createAdminUser = (data: CreateAdminUser, params: RequestParams = {}) =>
    this.request<void, any>({
      path: `/api/admin/admin/create`,
      method: "POST",
      body: data,
      type: ContentType.Json,
      ...params,
    });
  /**
   * No description
   *
   * @tags UserAdmin
   * @name ListUsers
   * @request POST:/api/admin/admin/list
   */
  listUsers = (data: QueryRequest, params: RequestParams = {}) =>
    this.request<UserSummaryPagedResponse, any>({
      path: `/api/admin/admin/list`,
      method: "POST",
      body: data,
      type: ContentType.Json,
      format: "json",
      ...params,
    });
}
