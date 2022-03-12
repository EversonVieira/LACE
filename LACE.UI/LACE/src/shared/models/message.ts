import { MessageTypeEnum } from "../models/message-type-enum";

export interface Message {
  code: number;
  messageType: MessageTypeEnum;
  text: string;
}


