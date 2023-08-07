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

import { CreateBinary, CreateProject, PagedRequest, ProjectSummaryPagedResponse } from "./data-contracts";
import { ContentType, HttpClient, RequestParams } from "./http-client";

export class Api<SecurityDataType = unknown> extends HttpClient<SecurityDataType> {
  /**
   * No description
   *
   * @tags Binary
   * @name ProjectsBinariesCreateCreate
   * @request POST:/api/projects/{projectId}/binaries/create
   */
  projectsBinariesCreateCreate = (projectId: string, data: CreateBinary, params: RequestParams = {}) =>
    this.request<string, any>({
      path: `/api/projects/${projectId}/binaries/create`,
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
   * @name ProjectsBinariesAnalyzeDetail
   * @request GET:/api/projects/{projectId}/binaries/{binaryId}/analyze
   */
  projectsBinariesAnalyzeDetail = (projectId: string, binaryId: string, params: RequestParams = {}) =>
    this.request<void, any>({
      path: `/api/projects/${projectId}/binaries/${binaryId}/analyze`,
      method: "GET",
      ...params,
    });
  /**
   * No description
   *
   * @tags Project
   * @name ProjectsListCreate
   * @request POST:/api/projects/list
   */
  projectsListCreate = (data: PagedRequest, params: RequestParams = {}) =>
    this.request<ProjectSummaryPagedResponse, any>({
      path: `/api/projects/list`,
      method: "POST",
      body: data,
      type: ContentType.Json,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags Project
   * @name ProjectsCreateCreate
   * @request POST:/api/projects/create
   */
  projectsCreateCreate = (data: CreateProject, params: RequestParams = {}) =>
    this.request<string, any>({
      path: `/api/projects/create`,
      method: "POST",
      body: data,
      type: ContentType.Json,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags Project
   * @name ProjectsDeleteCreate
   * @request POST:/api/projects/{projectId}/delete
   */
  projectsDeleteCreate = (projectId: string, params: RequestParams = {}) =>
    this.request<void, any>({
      path: `/api/projects/${projectId}/delete`,
      method: "POST",
      ...params,
    });
}
