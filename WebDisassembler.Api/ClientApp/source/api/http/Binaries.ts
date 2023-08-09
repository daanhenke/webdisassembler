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

import { CreateBinary } from "./data-contracts";
import { ContentType, HttpClient, RequestParams } from "./http-client";

export class Binaries<SecurityDataType = unknown> extends HttpClient<SecurityDataType> {
  /**
   * No description
   *
   * @tags Binary
   * @name BinariesCreateCreate
   * @summary Create
   * @request POST:/api/binaries/{projectId}/create
   */
  binariesCreateCreate = (projectId: string, data: CreateBinary, params: RequestParams = {}) =>
    this.request<string, any>({
      path: `/api/binaries/${projectId}/create`,
      method: "POST",
      body: data,
      type: ContentType.Json,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags Binary
   * @name Analyze
   * @request GET:/api/binaries/{projectId}/{binaryId}/analyze
   */
  analyze = (projectId: string, binaryId: string, params: RequestParams = {}) =>
    this.request<void, any>({
      path: `/api/binaries/${projectId}/${binaryId}/analyze`,
      method: "GET",
      ...params,
    });
}
