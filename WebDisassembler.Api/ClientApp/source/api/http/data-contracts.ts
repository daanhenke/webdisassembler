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

export interface BinarySummary {
  /** @format uuid */
  id?: string;
  name?: string | null;
}

export interface CreateBinary {
  name?: string | null;
  /** @format uuid */
  temporaryFileId?: string;
}

export interface CreateProject {
  name?: string | null;
}

export interface CurrentUser {
  /** @format uuid */
  userId: string;
  /** @minLength 1 */
  username: string;
}

export interface LoginRequest {
  usernameOrEmail?: string | null;
  password?: string | null;
}

export interface PagedRequest {
  /** @format int32 */
  index?: number;
  /**
   * @format int32
   * @default 10
   */
  size?: number;
}

export interface ProjectSummary {
  /** @format uuid */
  id?: string;
  name?: string | null;
  binaries?: BinarySummary[] | null;
}

export interface ProjectSummaryPagedResponse {
  /** @format int32 */
  total?: number | null;
  items?: ProjectSummary[] | null;
}
