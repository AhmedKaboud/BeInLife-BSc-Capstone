function [ProjectedImages Eigenfaces m S] = EigenfaceCore(T)
                                               % Use Principle Component Analysis (PCA) to determine the most %
                                               % discriminating features between images of faces.             % 
% Description: This function gets a 2D matrix, containing all training image vectors
% and returns 4 outputs which are extracted from training database.
%
% Argument:     T                       - A 2D matrix, containing all 1D image vectors.
%                                         Suppose all M images in the training path 
%                                         have the same size of NxN. So the length of 1D 
%                                         column vectors is N^2 and 'T' will be a N^2xM 2D matrix.
% 
% Returns:       m                      - (N^2x1) Mean of the training person images
%                Eigenfaces             - (N^2x(M-1)) Eigen vectors of the covariance matrix of the training person images
%                S                      -  S = M - 1 ;
%                ProjectedImages        - ((M-1)x(M-1)) Projection Matrix of centered images into facespace

 
%%%%%%%%%%%%%%%%%%%%%%%% Calculating the mean image 
m = mean(T,2); % Computing the average face image m = (1/M)*sum(Tj's)    (j = 1 : M)
Train_Number = size(T,2);

%%%%%%%%%%%%%%%%%%%%%%%% Calculating the deviation of each image from mean image
A = [];  
for i = 1 : Train_Number
    temp = double(T(:,i)) - m; % Computing the difference image for each image in the training set Ai = Ti - m
    A = [A temp]; % Merging all centered images
end

%%%%%%%%%%%%%%%%%%%%%%%% Snapshot method of Eigenface methos
% We know from linear algebra theory that for a PxQ matrix, the maximum
% number of non-zero eigenvalues that the matrix can have is min(P-1,Q-1).
% Since the number of training images (M) is usually less than the number
% of pixels (N*N), the most non-zero eigenvalues that can be found are equal
% to M-1. So we can calculate eigenvalues of A'*A (a MxM matrix) instead of
% A*A' (a N^2xN^2 matrix). It is clear that the dimensions of A*A' is much
% larger that A'*A. So the dimensionality will decrease.

L = A'*A; % L is the surrogate of covariance matrix C=A*A'.
[V D] = eig(L); % Diagonal elements of D are the eigenvalues for both L=A'*A and C=A*A'.

%%%%%%%%%%%%%%%%%%%%%%%% Sorting and eliminating eigenvalues
% All eigenvalues of matrix L are sorted and those who are less than a
% specified threshold, are eliminated. So the number of non-zero
% eigenvectors may be less than (M-1).

L_eig_vec = [];
for i = 1 : size(V,2) 
    if( D(i,i)>1 )
        L_eig_vec = [L_eig_vec V(:,i)];
    end
end

%%%%%%%%%%%%%%%%%%%%%%%% Calculating the eigenvectors of covariance matrix 'C'
% Eigenvectors of covariance matrix C (or so-called "Eigenfaces")
% can be recovered from L's eiegnvectors.
Eigenfaces = A * L_eig_vec; % A: centered image vectors
S = size(Eigenfaces,2);

ProjectedImages = [];
Train_Number = size(Eigenfaces,2);
for i = 1 : Train_Number
    temp = Eigenfaces'*A(:,i); % Projection of centered images into facespace
    ProjectedImages = [ProjectedImages temp]; 
end
end