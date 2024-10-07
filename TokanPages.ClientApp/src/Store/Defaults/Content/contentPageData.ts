import { OperationStatus } from "../../../Shared/enums";
import { ContentPageDataState } from "../../../Store/States";

export const ContentPageData: ContentPageDataState = {
    status: OperationStatus.notStarted,
    response: { 
        components: [] 
    }
}
