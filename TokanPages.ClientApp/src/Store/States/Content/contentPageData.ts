import { ComponentsDto } from "../../../Api/Models";
import { OperationStatus } from "../../../Shared/enums";

export interface ContentPageDataState {
    status: OperationStatus;
    isLoading: boolean;
    pageId: string | undefined;
    languageId?: string;
    components: ComponentsDto;
}
