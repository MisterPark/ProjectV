
void MainLight_half(float3 WorldPos, out half3 Direction, out half3 Color, out half DistanceAtten, out half ShadowAtten)
{
#ifdef SHADERGRAPH_PREVIEW			//! ���̴� �׷��� �����信�� ���̴� ���
	Direction = half3(0.5, 0.5, 0);
	Color = half3(0, 0, 0);
	DistanceAtten = 1;
	ShadowAtten = 1;
#else

#ifdef SHADOWS_SCREEN	//! Screen Space Shadow ����Ҷ� (NoȮ��)
	half4 clipPos = TransformWorldToHClip(WorldPos);
	half4 shadowCoord = ComputeScreenPos(clipPos);
#else
	half4 shadowCoord = TransformWorldToShadowCoord(WorldPos);	//! ���� ������ǥ�� �׸��� ���� ��ǥ�� ��ȯ�ϴ� �Լ� (NoȮ��)
#endif

	Light mainLight = GetMainLight();			//! Lighting.hlsl �Լ��� ����Ʈ ���� �� ������ ������ ����ü ����
	Direction = normalize(mainLight.direction);	//! ���� ����Ʈ ����
	Color = mainLight.color;				//! ���� ����Ʈ Į��

	DistanceAtten = mainLight.distanceAttenuation;	//! �ø�����ũ�� ���� �ø��Ǹ� 1, �ƴϸ� 0 (NoȮ��), ����Ʈ�� ��Ȳ���� �ٸ�
	//shadowAtten = mainLight.shadowAttenuation;		//! <��
	ShadowSamplingData shadowSamplingData = GetMainLightShadowSamplingData();	//! ������ ���谪
	half4 shadowParams = GetMainLightShadowParams();
	ShadowAtten = SampleShadowmap(TEXTURE2D_ARGS(_MainLightShadowmapTexture, sampler_MainLightShadowmapTexture), TransformWorldToShadowCoord(WorldPos), shadowSamplingData, shadowParams, false);
#endif
}