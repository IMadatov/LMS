/** Logged in user data */

export interface UserTelegramData {

    id: number|undefined;
  
    first_name?: string|undefined;
  
    last_name?: string|undefined;
  
    username?: string|undefined
  
    photo_url?: string|undefined
  
    auth_date: number|undefined;
  
    hash: string|undefined;
  
  }
  
  
  export type LOGIN_BUTTON_SIZE = 'medium' | 'large' | 'small';
  
  
  /** Configuration for a login button */
  
  export interface WidgetConfiguration {
  
    // Login button size. Default: large
  
    buttonStyle?: LOGIN_BUTTON_SIZE;
  
    // Show user photo near the button. Default: true
  
    showUserPhoto?: boolean;
  
    // Radius of buttons corners(0-20). Default: 20
  
    cornerRadius?: number;
  
    // Request for write access. Default: false
  
    accessToWriteMessages?: boolean
  
  }