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

import { GotoProjectCommand, GotoTenantCommand } from "./data-contracts";
import { HttpClient, RequestParams } from "./http-client";

export class CommandPalette<SecurityDataType = unknown> extends HttpClient<SecurityDataType> {
  /**
   * No description
   *
   * @tags CommandPalette
   * @name QueryCommands
   * @request GET:/api/command-palette/query
   */
  queryCommands = (
    query?: {
      query?: string;
    },
    params: RequestParams = {},
  ) =>
    this.request<(GotoProjectCommand | GotoTenantCommand)[], any>({
      path: `/api/command-palette/query`,
      method: "GET",
      query: query,
      format: "json",
      ...params,
    });
}
