import { BaseRequest } from "./base-request";
import { Filter } from "./filter";

export class ListRequest extends BaseRequest {
  limit: number = 0;
  pageIndex: number = 0;
}
