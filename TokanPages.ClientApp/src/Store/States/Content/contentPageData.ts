import { ComponentsDto } from "../../../Api/Models";
import { OperationStatus } from "../../../Shared/enums";

export interface ContentPageDataState {
    status: OperationStatus;
    isLoading: boolean;
    languageId?: string;
    components: ComponentsDto;
}
