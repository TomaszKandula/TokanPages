import { GetPageContentResultDto } from "../../../Api/Models";
import { OperationStatus } from "../../../Shared/enums";

export interface ContentPageDataState {
    status: OperationStatus;
    response: GetPageContentResultDto;
}
