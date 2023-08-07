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

import { CurrentUser, LoginRequest } from "./data-contracts";
import { ContentType, HttpClient, RequestParams } from "./http-client";

export class Authentication<SecurityDataType = unknown> extends HttpClient<SecurityDataType> {
  /**
   * No description
   *
   * @tags Authentication
   * @name Login
   * @request POST:/api/authentication/login
   */
  login = (data: LoginRequest, params: RequestParams = {}) =>
    this.request<void, any>({
      path: `/api/authentication/login`,
      method: "POST",
      body: data,
      type: ContentType.Json,
      ...params,
    });
  /**
   * No description
   *
   * @tags Authentication
   * @name Me
   * @request GET:/api/authentication/me
   */
  me = (params: RequestParams = {}) =>
    this.request<CurrentUser, any>({
      path: `/api/authentication/me`,
      method: "GET",
      format: "json",
      ...params,
    });
}
