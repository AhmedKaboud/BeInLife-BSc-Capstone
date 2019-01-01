function [ ProjectedImages  Eigenfaces m Eigenfaces_Size] = SignUp(TrainPath)
% SignUp step....
%
% Description: This function adds new person in the system
%
% Argument:      TrainPath                      - The path for trainning
%                                                 person images
%
% Returns:       m                              - (N^2x1) Mean of the training person
%                                                  images                  
%                Eigenfaces                     - (N^2x(M-1)) Eigen vectors of the covariance matrix of the 
%                                                  training person images
%                S                              -  S = M - 1 ;
%                ProjectedImages                - ((M-1)x(M-1)) Projection Matrix of centered images into facespace
T = Train(TrainPath);
[ProjectedImages Eigenfaces m Eigenfaces_Size] = EigenfaceCore(T); 
end

