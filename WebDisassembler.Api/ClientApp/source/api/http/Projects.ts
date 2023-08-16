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

import { CreateProject, PagedRequest, ProjectSummaryPagedResponse } from "./data-contracts";
import { ContentType, HttpClient, RequestParams } from "./http-client";

export class Projects<SecurityDataType = unknown> extends HttpClient<SecurityDataType> {
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
   * @name Delete
   * @request POST:/api/projects/{projectId}/delete
   */
  delete = (projectId: string, params: RequestParams = {}) =>
    this.request<void, any>({
      path: `/api/projects/${projectId}/delete`,
      method: "POST",
      ...params,
    });
  /**
   * No description
   *
   * @tags Project
   * @name FileTree
   * @request POST:/api/projects/{projectId}/filetree
   */
  fileTree = (projectId: string, params: RequestParams = {}) =>
    this.request<Record<string, any>, any>({
      path: `/api/projects/${projectId}/filetree`,
      method: "POST",
      format: "json",
      ...params,
    });
}
