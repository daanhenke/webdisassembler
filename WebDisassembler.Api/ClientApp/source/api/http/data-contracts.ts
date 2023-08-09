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
  projectPath?: string | null;
  /** @format uuid */
  fileId?: string;
}

export interface CreateProject {
  /** @format uuid */
  tenantId?: string;
  name?: string | null;
  shortDescription?: string | null;
}

export interface CreateTenant {
  /** @minLength 1 */
  name: string;
  /** @minLength 1 */
  subdomain: string;
  public: boolean;
}

export interface CurrentUser {
  /** @format uuid */
  userId: string;
  /** @minLength 1 */
  username: string;
  tenants: CurrentUserTenant[];
}

export interface CurrentUserTenant {
  /** @format uuid */
  tenantId: string;
  /** @minLength 1 */
  name: string;
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
  shortDescription?: string | null;
  binaries?: BinarySummary[] | null;
}

export interface ProjectSummaryPagedResponse {
  /** @format int32 */
  total?: number;
  items?: ProjectSummary[] | null;
}

export interface TenantSummary {
  /** @format uuid */
  id?: string;
  name?: string | null;
  subdomain?: string | null;
}

export interface TenantSummaryPagedResponse {
  /** @format int32 */
  total?: number;
  items?: TenantSummary[] | null;
}
